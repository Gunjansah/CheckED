using System.Collections.ObjectModel;

namespace CheckED;

public partial class UserDashboard : ContentPage
{

    public ObservableCollection<Event> UpcomingEvents { get; set; }
    public ObservableCollection<Event> RegisteredEvents { get; set; }

    DatabaseHelper database;
    public UserDashboard()
	{
		InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Users.db3");
        database = new DatabaseHelper(dbPath);

        
        UpcomingEvents = new ObservableCollection<Event>();
        RegisteredEvents = new ObservableCollection<Event>();


        InsertPredefinedEventsAsync();

        BindingContext = this;

    }

    public async Task InsertPredefinedEventsAsync()
    {
        var existingEvents = await database.GetAllEventsAsync();
        if (existingEvents != null && existingEvents.Count > 0)
        {
            Console.WriteLine("Events already exist. Skipping insertion.");
           
        }
        else
        {
            var predefinedEvents = new List<Event>
    {
        new Event { UserId = 1, EventName = "Music Concert", EventDate = "Oct 15, 2024", EventDescription = "Join us for an amazing music experience.", ImageUrl = "event1.png", NumGoing = 0, RegistrationFormLink = "http://example.com" },
        new Event { UserId = 2, EventName = "Tech Conference", EventDate = "Nov 10, 2024", EventDescription = "Learn the latest trends in technology.", ImageUrl = "event2.png", NumGoing = 0, RegistrationFormLink = "http://example.com" },
        new Event { UserId = 3, EventName = "Art Exhibition", EventDate = "Dec 5, 2024", EventDescription = "Explore beautiful artwork from local artists.", ImageUrl = "event3.png", NumGoing = 0, RegistrationFormLink = "http://example.com" },
        new Event { UserId = 4, EventName = "Sports", EventDate = "Dec 5, 2024", EventDescription = "Enjoy a day of sports and activities.", ImageUrl = "event4.png", NumGoing = 0, RegistrationFormLink = "http://example.com" }
    };

            foreach (var evnt in predefinedEvents)
            {

                await database.SaveEventAsync(evnt);
            }

            Console.WriteLine("Predefined events inserted successfully.");
        }


        var events = await database.GetAllEventsAsync();

        UpcomingEvents.Clear();

        foreach (var evnt in events)
        {
            UpcomingEvents.Add(evnt);
        }


    }





        private void OnToggleDarkModeClicked(object sender, EventArgs e)
    {

    }

    private async void BtnRegister(object sender, EventArgs e)
    {
        var selectedEvent = (sender as Button)?.BindingContext as Event;
        if (selectedEvent != null)
        {
            try
            {
                selectedEvent.NumGoing++; // Update the registration count for the event

                // Update the event in the database
                await database.UpdateEventAsync(selectedEvent);

                // Add the event to the RegisteredEvents collection
                RegisteredEvents.Add(selectedEvent);

                // Optionally, you can remove it from UpcomingEvents if needed
                UpcomingEvents.Remove(selectedEvent);

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
        var selectedEvent = (sender as Button)?.BindingContext as Event;
        if (selectedEvent != null)
        {
            try
            {
                selectedEvent.NumGoing--; // Update the registration count for the event

                // Update the event in the database
                await database.UpdateEventAsync(selectedEvent);

                // Add the event to the RegisteredEvents collection
                UpcomingEvents.Add(selectedEvent);

                // Optionally, you can remove it from UpcomingEvents if needed
                RegisteredEvents.Remove(selectedEvent);

                await DisplayAlert("Success", $"You have  unregistered from {selectedEvent.EventName}!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to register for the event: {ex.Message}", "OK");
            }
        }
    }

    private void OnHamburgerClicked(object sender, EventArgs e)
    {
        SidebarOptions.IsVisible = !SidebarOptions.IsVisible; // Toggle compact sidebar menu visibility
    }

    private void AccountSettings_Clicked(object sender, EventArgs e)
    {
        // Logic to open account settings
    }


    private void BtnCheckIn(object sender, EventArgs e)
    {
        // Logic for check-in
    }

    private void RegisterEvent(object sender, EventArgs e)
    {
        
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {

    }

    private async void OnMoreInfoClicked(object sender, EventArgs e)
    {
        // Get the selected event data from the button's BindingContext
        var selectedEvent = (sender as Button)?.BindingContext as Event;
        if (selectedEvent != null)
        {
            // Navigate to EventDetails page, passing the selected event as a parameter
            await Navigation.PushAsync(new EventDetails(selectedEvent));
        }
    }
}

