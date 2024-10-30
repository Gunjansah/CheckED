namespace CheckED;

public partial class EventDetails : ContentPage
{
    public EventDetails(Event selectedEvent)
    {
        InitializeComponent();
        BindingContext = selectedEvent;
    }

    private void OnRegisterClicked(object sender, EventArgs e)
    {

    }

    private void OnCheckInClicked(object sender, EventArgs e)
    {

    }

    private void OnShowQRCodeClicked(object sender, EventArgs e)
    {

    }

    private void OnEditEventClicked(object sender, EventArgs e)
    {

    }

    private void OnHamburgerClicked(object sender, EventArgs e)
    {
        SidebarOptions.IsVisible = !SidebarOptions.IsVisible;
    }
}