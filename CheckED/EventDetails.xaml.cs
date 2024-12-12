

using System.Collections.ObjectModel;

namespace CheckED;

public partial class EventDetails : ContentPage
{
   

    DatabaseHelper database;
    private Event selectedEvent;
    public string CurrentUserName => UserSession.UserName;

    public EventDetails(Event selectedEvent)
    {
        InitializeComponent();



        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Users.db3");
        database = new DatabaseHelper(dbPath);

        this.selectedEvent = selectedEvent;
        BindingContext = selectedEvent;


        LoadRegisteredEventsAsync();

        EditButton.IsVisible = UserSession.UserId == selectedEvent.CreatorId;
    }

    private async Task LoadRegisteredEventsAsync()
    {

        int userId = UserSession.UserId;

        var registeredEvents = await database.GetRegisteredEventsUserAsync(userId);

        bool isRegistered = registeredEvents.Any(re => re.EventId == selectedEvent.EventId);

        if (isRegistered)
        {
            Register.IsVisible = false;  // Hide register button if already registered
            Unregister.IsVisible = true; // Show unregister button (optional)
        }
        else
        {
            Register.IsVisible = true;  // Show register button if not registered
            Unregister.IsVisible = false; // Hide unregister button (optional)
        }

    }

    private async void BtnRegister(object sender, EventArgs e)
    {

        if (selectedEvent != null)
        {
            try
            {

                selectedEvent.NumGoing++;


                await database.UpdateEventAsync(selectedEvent);


                await database.UpdateUserRegisteredEventsAsync(UserSession.UserId, selectedEvent.EventId, true);

                LoadRegisteredEventsAsync();

                await DisplayAlert("Success", $"You have registered for {selectedEvent.EventName}!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to register for the event: {ex.Message}", "OK");
            }

        }
    }

    private async void BtnUnregister(object sender, EventArgs e)
    {

        if (selectedEvent != null)
        {
            try
            {

                selectedEvent.NumGoing--;

 
                await database.UpdateEventAsync(selectedEvent);

                // Remove the user from the registered events table in the database
                await database.UpdateUserRegisteredEventsAsync(UserSession.UserId, selectedEvent.EventId, false);

                // Update UI after unregistration
                LoadRegisteredEventsAsync();

                await DisplayAlert("Success", $"You have unregistered from {selectedEvent.EventName}.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to unregister from the event: {ex.Message}", "OK");
            }
        }
    }

    private void OnCheckInClicked(object sender, EventArgs e)
    {

    }

    private void OnShowQRCodeClicked(object sender, EventArgs e)
    {

    }

    private async void OnAccountSettingsClicked(object sender, EventArgs e)
    {
        var accountSettingsPage = new AccountSettingsPage(database); // Pass the DatabaseHelper instance
        await Navigation.PushAsync(accountSettingsPage);
        SidebarOptions.IsVisible = false; // Hide sidebar after navigation
    }

    private async void OnContactUsClicked(object sender, EventArgs e)
    {
        var contactUsPage = new ContactUsPage(database); // Pass the DatabaseHelper instance
        await Navigation.PushAsync(contactUsPage);
        SidebarOptions.IsVisible = false; // Hide sidebar after navigation
    }


    // Handler for Logout button
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (confirm)
        {
            UserSession.Clear(); // Clear user session
            await Navigation.PopToRootAsync(); // Navigate back to the Login Page
        }
    }

    private async void OnEditEventClicked(object sender, EventArgs e)
    {
        var selectedEvent = (sender as Button)?.BindingContext as Event;
        if (selectedEvent != null)
        {

            await Navigation.PushAsync(new EditEvent(selectedEvent));
        }
    }

    private void OnHamburgerClicked(object sender, EventArgs e)
    {
        SidebarOptions.IsVisible = !SidebarOptions.IsVisible;
    }


}