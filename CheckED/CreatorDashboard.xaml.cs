using System.Collections.ObjectModel;

namespace CheckED;
public partial class CreatorDashboard : ContentPage
{
    public ObservableCollection<CEvent> YourEvents { get; set; }

    public CreatorDashboard()
	{
		InitializeComponent();
        YourEvents = new ObservableCollection<CEvent>
        {
            new CEvent { CImageUrl = "cevent1.png", CEventName = "Music Concert", CEventDate = "Oct 15, 2024", CEventDescription = "Join us for an amazing music experience. iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii" },
            new CEvent { CImageUrl = "cevent2.png", CEventName = "Tech Conference", CEventDate = "Nov 10, 2024", CEventDescription = "Learn the latest trends in technology." },
            new CEvent { CImageUrl = "cevent3.png", CEventName = "Art Exhibition", CEventDate = "Dec 5, 2024", CEventDescription = "Explore beautiful artwork from local artists." },
            new CEvent { CImageUrl = "cevent4.png", CEventName = "Sports", CEventDate = "Dec 5, 2024", CEventDescription = "Explore beautiful artwork from local artists." }
        };

        BindingContext = this;
    }

    private async void BtnCreateEvent(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddEvent());

    }

    private void BtnMoreInfo(object sender, EventArgs e)
    {

    }
}
public class CEvent
{
    public string CImageUrl { get; set; }
    public string CEventName { get; set; }
    public string CEventDate { get; set; }
    public string CEventDescription { get; set; }
}
