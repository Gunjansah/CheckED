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
}

public class Event
{
    public string ImageUrl { get; set; }
    public string EventName { get; set; }
    public string EventDate { get; set; }
    public string EventDescription { get; set; }
}
