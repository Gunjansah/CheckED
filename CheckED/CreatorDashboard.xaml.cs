using System.Collections.ObjectModel;

namespace CheckED;



public partial class CreatorDashboard : ContentPage
{
    public ObservableCollection<Event> YourEvents { get; set; }
    public ObservableCollection<Event> SearchedEvents { get; set; }


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
}
