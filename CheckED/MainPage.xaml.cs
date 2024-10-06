namespace CheckED
{
    public partial class MainPage : ContentPage
    {
        public MainPage() => InitializeComponent();

        private async void UserBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }


        private async void CreatorBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage()); // Need to make login page connecting to different database
        }

        private async void GuestBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page()); // Need to link to user homepage without login
        }
    }

}
