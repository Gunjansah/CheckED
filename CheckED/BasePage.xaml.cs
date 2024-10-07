using Microsoft.Maui.Controls;

namespace CheckED;

public partial class BasePage : ContentPage
{
	public BasePage()
	{
		InitializeComponent();
	}

    protected async void OnMenuClicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Profile", "Contact Me", "Logout");
        switch (action)
        {
            case "Profile":
                await Navigation.PushAsync(new Profile());
                break;
            case "Contact Me":
                await Navigation.PushAsync(new ContactMe());
                break;
            case "Logout":
                await DisplayAlert("Logout", "You have logged out", "OK");
                break;
        }
    }
}