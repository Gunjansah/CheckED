using System.Collections.ObjectModel;

namespace CheckED;

public partial class UserDashboard : ContentPage
{

    public ObservableCollection<Event> UpcomingEvents { get; set; }
    public ObservableCollection<Event> RegisteredEvents { get; set; }

    public ObservableCollection<Event> SearchedUpcomingEvents { get; set; }

    public ObservableCollection<Event> SearchedRegisteredEvents { get; set; }

    DatabaseHelper database;
    public UserDashboard()
	{
		InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Users.db3");
        database = new DatabaseHelper(dbPath);

        
        UpcomingEvents = new ObservableCollection<Event>();
        RegisteredEvents = new ObservableCollection<Event>();

        SearchedUpcomingEvents = new ObservableCollection<Event>();
        SearchedRegisteredEvents = new ObservableCollection<Event>();


        InsertPredefinedEventsAsync();
        LoadRegisteredEventsAsync();

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
        new Event { CreatorId = 1, EventName = "Music Concert", EventDate = "Oct 15, 2024", EventDescription = "Join us for an amazing music experience.", ImageUrl = "event1.png", NumGoing = 0, RegistrationFormLink = "http://example.com" },
        new Event { CreatorId = 2, EventName = "Tech Conference", EventDate = "Nov 10, 2024", EventDescription = "Learn the latest trends in technology.", ImageUrl = "event2.png", NumGoing = 0, RegistrationFormLink = "http://example.com" },
        new Event { CreatorId = 3, EventName = "Art Exhibition", EventDate = "Dec 5, 2024", EventDescription = "Explore beautiful artwork from local artists.", ImageUrl = "event3.png", NumGoing = 0, RegistrationFormLink = "http://example.com" },
        new Event { CreatorId = 4, EventName = "Sports", EventDate = "Dec 5, 2024", EventDescription = "Enjoy a day of sports and activities.", ImageUrl = "event4.png", NumGoing = 0, RegistrationFormLink = "http://example.com" }
    };

            foreach (var evnt in predefinedEvents)
            {

                await database.SaveEventAsync(evnt);
            }

            Console.WriteLine("Predefined events inserted successfully.");
        }


        var events = await database.GetAllEventsAsync();

        var userId = UserSession.UserId;
        var registeredEvents = await database.GetRegisteredEventsUserAsync(userId);

        UpcomingEvents.Clear();

        foreach (var evnt in events)
        {

            if (!registeredEvents.Any(re => re.EventId == evnt.EventId))
            {
                UpcomingEvents.Add(evnt);
            }
        }

        SearchedUpcomingEvents = new ObservableCollection<Event>(UpcomingEvents);

        OnPropertyChanged(nameof(SearchedUpcomingEvents));


    }

    public async Task LoadRegisteredEventsAsync()
    {
    
        int userId = UserSession.UserId; 

        var registeredEvents = await database.GetRegisteredEventsUserAsync(userId);

        RegisteredEvents.Clear();

        foreach (var evnt in registeredEvents)
        {
            RegisteredEvents.Add(evnt);
        }

        SearchedRegisteredEvents = new ObservableCollection<Event>(RegisteredEvents);

        OnPropertyChanged(nameof(SearchedRegisteredEvents));

    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? string.Empty;

        if (string.IsNullOrEmpty(searchText))
        {
            SearchedUpcomingEvents = new ObservableCollection<Event>(UpcomingEvents);
            SearchedRegisteredEvents = new ObservableCollection<Event>(RegisteredEvents);
        }
        else
        {

            // Search Upcoming Events
            var filteredUpcoming = UpcomingEvents.Where(evnt => evnt.EventName.ToLower().Contains(searchText)).ToList();
            SearchedUpcomingEvents = new ObservableCollection<Event>(filteredUpcoming);

            // Search Registered Events
            var filteredRegistered = RegisteredEvents.Where(evnt => evnt.EventName.ToLower().Contains(searchText)).ToList();
            SearchedRegisteredEvents = new ObservableCollection<Event>(filteredRegistered);
        }

        OnPropertyChanged(nameof(SearchedUpcomingEvents));
        OnPropertyChanged(nameof(SearchedRegisteredEvents));
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
                selectedEvent.NumGoing++; 

                await database.UpdateEventAsync(selectedEvent);

              
                await database.UpdateUserRegisteredEventsAsync(UserSession.UserId, selectedEvent.EventId, true);



                RegisteredEvents.Add(selectedEvent);
                SearchedRegisteredEvents.Add(selectedEvent);

                OnPropertyChanged(nameof(RegisteredEvents));
                OnPropertyChanged(nameof(SearchedRegisteredEvents));


                UpcomingEvents.Remove(selectedEvent);
                SearchedUpcomingEvents.Remove(selectedEvent);

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
                selectedEvent.NumGoing--; 

          
                await database.UpdateEventAsync(selectedEvent);

                await database.UpdateUserRegisteredEventsAsync(UserSession.UserId, selectedEvent.EventId, false);

              
                UpcomingEvents.Add(selectedEvent);
                SearchedUpcomingEvents.Add(selectedEvent);

                OnPropertyChanged(nameof(UpcomingEvents));
                OnPropertyChanged(nameof(SearchedUpcomingEvents));


                RegisteredEvents.Remove(selectedEvent);
                SearchedRegisteredEvents.Remove(selectedEvent);

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
        SidebarOptions.IsVisible = !SidebarOptions.IsVisible; 
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
      
        var selectedEvent = (sender as Button)?.BindingContext as Event;
        if (selectedEvent != null)
        {
          
            await Navigation.PushAsync(new EventDetails(selectedEvent));
        }
    }
}

