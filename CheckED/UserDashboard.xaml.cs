using System.Collections.ObjectModel;

namespace CheckED;

public partial class UserDashboard : ContentPage
{
    public ObservableCollection<Event> UpcomingEvents { get; set; }
    public UserDashboard()
	{
		InitializeComponent();
        UpcomingEvents = new ObservableCollection<Event>
        {
            new Event { ImageUrl = "event1.png", EventName = "Music Concert", EventDate = "Oct 15, 2024", EventDescription = "Join us for an amazing music experience. iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii" },
            new Event { ImageUrl = "event2.png", EventName = "Tech Conference", EventDate = "Nov 10, 2024", EventDescription = "Learn the latest trends in technology." },
            new Event { ImageUrl = "event3.png", EventName = "Art Exhibition", EventDate = "Dec 5, 2024", EventDescription = "Explore beautiful artwork from local artists." },
            new Event { ImageUrl = "event4.png", EventName = "Sports", EventDate = "Dec 5, 2024", EventDescription = "Explore beautiful artwork from local artists." }
        };

        BindingContext = this;
    }

    private void OnToggleDarkModeClicked(object sender, EventArgs e)
    {

    }

    private void BtnRegister(object sender, EventArgs e)
    {

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

public class Event
{
    public string ImageUrl { get; set; }
    public string EventName { get; set; }
    public string EventDate { get; set; }
    public string EventDescription { get; set; }
}
