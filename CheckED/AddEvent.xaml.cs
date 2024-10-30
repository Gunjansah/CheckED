namespace CheckED;

public partial class AddEvent : ContentPage
{
	public AddEvent()
	{
		InitializeComponent();
	}


    private void AddTheEvent(object sender, EventArgs e)
    {

    }



    private void BrowseFiles(object sender, EventArgs e)
    {

    }

    private void OnHamburgerClicked(object sender, EventArgs e)
    {
        SidebarOptions.IsVisible = !SidebarOptions.IsVisible;
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {

    }
}