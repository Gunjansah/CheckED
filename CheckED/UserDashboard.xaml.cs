using System.Collections.ObjectModel;

namespace CheckED;

public partial class UserDashboard : ContentPage
{
    public ObservableCollection<Event> UpcomingEvents { get; set; }

    DatabaseHelper database;
    public UserDashboard()
	{
		InitializeComponent();
        UpcomingEvents = new ObservableCollection<Event>
        {
            new Event
    {
        Id = 1, // Auto-generated if using SQLite
        UserId = "user1@example.com", // Replace with actual user ID or email
        EventName = "Music Concert",
        EventDate = "Oct 15, 2024",
        EventDescription = "Join us for an amazing music experience.",
        ImageUrl = "event1.png",
        NumGoing = 0, // Default initial value
        RegistrationFormLink = "https://example.com/register/music-concert" // Replace with actual link
    },
    new Event
    {
        Id = 2, // Auto-generated if using SQLite
        UserId = "user2@example.com", // Replace with actual user ID or email
        EventName = "Tech Conference",
        EventDate = "Nov 10, 2024",
        EventDescription = "Learn the latest trends in technology.",
        ImageUrl = "event2.png",
        NumGoing = 0, // Default initial value
        RegistrationFormLink = "https://example.com/register/tech-conference" // Replace with actual link
    },
    new Event
    {
        Id = 3, // Auto-generated if using SQLite
        UserId = "user3@example.com", // Replace with actual user ID or email
        EventName = "Art Exhibition",
        EventDate = "Dec 5, 2024",
        EventDescription = "Explore beautiful artwork from local artists.",
        ImageUrl = "event3.png",
        NumGoing = 0, // Default initial value
        RegistrationFormLink = "https://example.com/register/art-exhibition" // Replace with actual link
    },
    new Event
    {
        Id = 4, // Auto-generated if using SQLite
        UserId = "user4@example.com", // Replace with actual user ID or email
        EventName = "Sports Event",
        EventDate = "Dec 5, 2024",
        EventDescription = "Join us for an exciting sports event.",
        ImageUrl = "event4.png",
        NumGoing = 0, // Default initial value
        RegistrationFormLink = "https://example.com/register/sports-event" // Replace with actual link
    }
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

    private async void RegisterEvent(object sender, EventArgs e)
    {
        var selectedEvent = (sender as Button)?.BindingContext as Event;
        if (selectedEvent != null)
        {
            try
            {
          
                selectedEvent.NumGoing++;

                await database.UpdateEventAsync(selectedEvent);

                await DisplayAlert("Success", $"You have registered for {selectedEvent.EventName}!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to register for the event: {ex.Message}", "OK");
            }
        }
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

