using CloudKit;

namespace CheckED;

public partial class EventDetails : ContentPage
{
    public EventDetails(Event selectedEvent)
    {
        InitializeComponent();
        BindingContext = selectedEvent;
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        
            var selectedEvent = (sender as Button)?.BindingContext as Event;
            if (selectedEvent != null)
            {
                try
                {
                    selectedEvent.NumGoing++; 


                    await DisplayAlert("Success", $"You have registered for {selectedEvent.EventName}!", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to register for the event: {ex.Message}", "OK");
                }
            }
        
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