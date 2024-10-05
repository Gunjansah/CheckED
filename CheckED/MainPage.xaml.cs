namespace CheckED
{
    public partial class MainPage : ContentPage
    {
            public MainPage()
        {
            InitializeComponent();
        }

  
            private async void UserBtnClicked(object sender, EventArgs e)
            {
                await Navigation.PushAsync(new LoginPage());
            }
        

        private void CreatorBtnClicked(object sender, EventArgs e)
        {

        }

        private void GuestBtnClicked(object sender, EventArgs e)
        {

        }
    }

}
