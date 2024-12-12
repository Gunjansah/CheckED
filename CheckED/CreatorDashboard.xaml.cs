using System.Collections.ObjectModel;

namespace CheckED;



public partial class CreatorDashboard : ContentPage
{
    public ObservableCollection<Event> YourEvents { get; set; }
    public ObservableCollection<Event> SearchedEvents { get; set; }

    public string CurrentUserName => UserSession.UserName;

    DatabaseHelper database;
    public CreatorDashboard()
	{
		InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Users.db3");
        database = new DatabaseHelper(dbPath);

        YourEvents = new ObservableCollection<Event>();
        SearchedEvents = new ObservableCollection<Event>();

        InsertYourEventsAsync();
      

        BindingContext = this;
    }

    public async Task InsertYourEventsAsync()
    {

        var events = await database.GetAllEventsAsync();

        YourEvents.Clear();

        foreach (var evnt in events)
        {
            if ( evnt.CreatorId == UserSession.UserId)
            {
                YourEvents.Add(evnt);
            }
           
        }

        SearchedEvents = new ObservableCollection<Event>(YourEvents);

        OnPropertyChanged(nameof(SearchedEvents));


    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? string.Empty;

        if (string.IsNullOrEmpty(searchText))
        {
        
            SearchedEvents = new ObservableCollection<Event>(YourEvents);
        }
        else
        {
     
            var filtered = YourEvents.Where(evnt => evnt.EventName.ToLower().Contains(searchText)).ToList();
            SearchedEvents = new ObservableCollection<Event>(filtered);
        }


        OnPropertyChanged(nameof(SearchedEvents));
    }



    private async void BtnCreateEvent(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEvent());

    }

    private async void BtnMoreInfo(object sender, EventArgs e)
    {
        var selectedEvent = (sender as Button)?.BindingContext as Event;
        if (selectedEvent != null)
        {
            // Navigate to EventDetails page, passing the selected event as a parameter
            await Navigation.PushAsync(new EventDetails(selectedEvent));
        }
    }

    private void OnHamburgerClicked(object sender, EventArgs e)
    {
        SidebarOptions.IsVisible = !SidebarOptions.IsVisible;
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
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
}
