using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace CheckED
{
    public partial class AccountSettingsPage : ContentPage
    {
        private readonly DatabaseHelper _database;
        private ImageSource _profileImageSource;
        public AccountSettingsPage(DatabaseHelper database)
        {
            InitializeComponent();
            _database = database;
            BindingContext = this; // Set BindingContext to this instance
            LoadUserData();
        }

        public ImageSource ProfileImageSource
        {
            get => _profileImageSource;
            set
            {
                _profileImageSource = value;
                OnPropertyChanged(nameof(ProfileImageSource));
            }
        }


        // Property Bindings
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _userEmail;
        public string UserEmail
        {
            get => _userEmail;
            set
            {
                _userEmail = value;
                OnPropertyChanged(nameof(UserEmail));
            }
        }

        private string _userPhone;
        public string UserPhone
        {
            get => _userPhone;
            set
            {
                _userPhone = value;
                OnPropertyChanged(nameof(UserPhone));
            }
        }

        private async void LoadUserData()
        {
            // Ensure that UserSession.UserId is set correctly
            var user = await _database.GetUserByIdAsync(UserSession.UserId);
            if (user != null)
            {
                UserName = user.Name;
                UserEmail = user.Email;
                UserPhone = user.Phone;

                ProfileImageSource = ImageSource.FromFile("profile_placeholder.png");
            }
            else
            {
                await DisplayAlert("Error", "Unable to load user data.", "OK");
            }
        }

        private async void OnEditProfileClicked(object sender, EventArgs e)
        {
            try
            {
                // Open file picker to select an image
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select Profile Image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    // Load the selected image
                    var stream = await result.OpenReadAsync();
                    ProfileImageSource = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., user cancellation)
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        

        private void OnChangePasswordClicked(object sender, EventArgs e)
        {
            // Navigate to change password page or show change password dialog
            DisplayAlert("Change Password", "Password change functionality will be implemented soon.", "OK");
        }

        private void OnTwoFactorToggled(object sender, ToggledEventArgs e)
        {
            // Implement two-factor authentication toggle logic
            DisplayAlert("Two-Factor Authentication",
                $"Two-Factor Authentication is now {(e.Value ? "Enabled" : "Disabled")}", "OK");
        }

        private void OnNotificationToggled(object sender, ToggledEventArgs e)
        {
            // Implement notification preferences toggle logic
            DisplayAlert("Notifications",
                $"Email Notifications are now {(e.Value ? "Enabled" : "Disabled")}", "OK");
        }

        private async void OnDeactivateAccountClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (confirm)
            {
                UserSession.Clear(); // Clear user session
                await Navigation.PopToRootAsync(); // Navigate back to the Login Page
            }
        }
    }
}
