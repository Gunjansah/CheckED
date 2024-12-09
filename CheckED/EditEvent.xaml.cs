using System.Text;
using Microsoft.Maui.Controls;
using System;
using System.Security.Cryptography;
using System.ComponentModel;


namespace CheckED;

public partial class EditEvent : ContentPage
{
    DatabaseHelper database;
    private Event editingEvent;
    public EditEvent(Event eventToEdit)
    {
        InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Users.db3");
        database = new DatabaseHelper(dbPath);

        editingEvent = eventToEdit;
        DateTime eventDate = DateTime.ParseExact(editingEvent.EventDate, "MMM dd, yyyy", System.Globalization.CultureInfo.InvariantCulture);


        EventName.Text = editingEvent.EventName;
        EventDescription.Text = editingEvent.EventDescription;
        EventFormLink.Text = editingEvent.RegistrationFormLink;
        EventMediaFile.Text = editingEvent.ImageUrl;
        EventDatePicker.Date = eventDate;
        

    }


    async void EditTheEvent(object sender, EventArgs e)
    {
        try
        {
            editingEvent.EventName = EventName.Text;
            editingEvent.EventDate = EventDatePicker.Date.ToString("MMM dd yyyy"); 
            editingEvent.EventDescription = EventDescription.Text;

            await database.UpdateEventAsync(editingEvent);

            // Provide feedback to the user
            await DisplayAlert("Success", "Event added successfully!", "OK");
            await Navigation.PushAsync(new CreatorDashboard());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add event: {ex.Message}", "OK");
        }
    }




    private async void BrowseFiles(object sender, EventArgs e)
    {
        try
        {

            var file = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select an image",
                FileTypes = FilePickerFileType.Images
            });

            if (file != null)
            {
                string fileName = file.FileName;
                string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);


                using (var stream = await file.OpenReadAsync())
                using (var newFile = File.OpenWrite(filePath))
                {
                    await stream.CopyToAsync(newFile);
                }

                EventMediaFile.Text = filePath;
            }
            else
            {
                await DisplayAlert("No File Selected", "Please select an image file.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }


    private void OnHamburgerClicked(object sender, EventArgs e)
    {
        SidebarOptions.IsVisible = !SidebarOptions.IsVisible;
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {

    }


}