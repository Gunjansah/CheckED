namespace CheckED;

public partial class LoginPageCreator : ContentPage
{
    public LoginPageCreator()
    {
        InitializeComponent();
    }

    private void UserEmail(object sender, TextChangedEventArgs e)
    {

    }

    private void UserPassword(object sender, TextChangedEventArgs e)
    {

    }

    private void BtnForgotPassword(object sender, EventArgs e)
    {

    }

    private async void BtnCreateAcc(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAccount());
    }


    private async void BtnUserSignIn(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreatorDashboard());
    }
}