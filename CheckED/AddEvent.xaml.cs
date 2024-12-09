using System.Text;
using Microsoft.Maui.Controls;
using System;
using System.Security.Cryptography;
using System.ComponentModel;


namespace CheckED;

public partial class AddEvent : ContentPage
{
    DatabaseHelper database;
    public AddEvent()
	{
		InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Users.db3");
        database = new DatabaseHelper(dbPath);

       
    }


    async void AddTheEvent(object sender, EventArgs e)
    {
        try
        {
            
            string eventName = EventName.Text; // Event Name Entry
            string eventMediaFile = EventMediaFile.Text; // Media File Path Entry
            string eventDescription = EventDescription.Text; // Event Description Editor
            string eventFormLink = EventFormLink.Text; // Registration Form Link Entry
            string eventDate = EventDatePicker.Date.ToString("MMM dd, yyyy"); // Example static date; replace with actual date picker if available
            int creatorId = UserSession.UserId; // Replace with the current logged-in user's ID

            // Validate the inputs
            if (string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(eventDescription))
            {
                await DisplayAlert("Error", "Please fill in all required fields.", "OK");
                return;
            }

            var newEvent = new Event
            {
                CreatorId = creatorId,
                EventName = eventName,
                EventDate = eventDate,
                EventDescription = eventDescription,
                ImageUrl = eventMediaFile,
                NumGoing = 0,
                RegistrationFormLink = eventFormLink
            };

            // Save the event to the database
            await database.SaveEventAsync(newEvent);

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