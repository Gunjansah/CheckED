using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using QRCoder;
using System.IO;

namespace CheckED
{
    public partial class EventDetails : ContentPage
    {
        DatabaseHelper database;
        private Event selectedEvent;
        private bool isRegistered;

        public EventDetails(Event selectedEvent)
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Users.db3");
            database = new DatabaseHelper(dbPath);
            this.selectedEvent = selectedEvent;
            BindingContext = selectedEvent;
            LoadRegisteredEventsAsync();
        }

        private async Task LoadRegisteredEventsAsync()
        {
            int userId = UserSession.UserId;
            var registeredEvents = await database.GetRegisteredEventsUserAsync(userId);
            isRegistered = registeredEvents.Any(re => re.EventId == selectedEvent.EventId);
            UpdateButtonVisibility();
        }

        private void UpdateButtonVisibility()
        {
            if (isRegistered)
            {
                Register.IsVisible = false;
                Unregister.IsVisible = true;
                CheckInButton.IsVisible = true;
            }
            else
            {
                Register.IsVisible = true;
                Unregister.IsVisible = false;
                CheckInButton.IsVisible = false;
            }
            QRCodeFrame.IsVisible = false;
        }

        private async void BtnRegister(object sender, EventArgs e)
        {
            if (selectedEvent != null)
            {
                try
                {
                    selectedEvent.NumGoing++;
                    await database.UpdateEventAsync(selectedEvent);
                    await database.UpdateUserRegisteredEventsAsync(UserSession.UserId, selectedEvent.EventId, true);
                    isRegistered = true;
                    UpdateButtonVisibility();
                    await DisplayAlert("Success", $"You have registered for {selectedEvent.EventName}!", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to register for the event: {ex.Message}", "OK");
                }
            }
        }

        private async void BtnUnregister(object sender, EventArgs e)
        {
            if (selectedEvent != null)
            {
                try
                {
                    selectedEvent.NumGoing--;
                    await database.UpdateEventAsync(selectedEvent);
                    await database.UpdateUserRegisteredEventsAsync(UserSession.UserId, selectedEvent.EventId, false);
                    isRegistered = false;
                    UpdateButtonVisibility();
                    await DisplayAlert("Success", $"You have unregistered from {selectedEvent.EventName}.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to unregister from the event: {ex.Message}", "OK");
                }
            }
        }

        private async void OnCheckInClicked(object sender, EventArgs e)
        {
            if (isRegistered)
            {
                try
                {
                    string qrValue = GenerateUniqueQRValue();
                    QRCodeValue.Text = qrValue;

                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrValue, QRCodeGenerator.ECCLevel.Q);
                    PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
                    byte[] qrCodeBytes = qRCode.GetGraphic(20);

                    QRCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
                    QRCodeFrame.IsVisible = true;

                    await SaveQRCodeValue(qrValue);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to generate QR code: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "You must be registered for this event to check in.", "OK");
            }
        }

        private string GenerateUniqueQRValue()
        {
            string timestamp = DateTime.UtcNow.Ticks.ToString();
            string uniqueId = Guid.NewGuid().ToString("N");
            return $"EVENT:{selectedEvent.EventId}|USER:{UserSession.UserId}|TIME:{timestamp}|ID:{uniqueId}";
        }

        private async Task SaveQRCodeValue(string qrValue)
        {
            try
            {
                // TODO: Implement saving QR code value to your database
                // await database.SaveQRCodeAsync(selectedEvent.EventId, UserSession.UserId, qrValue);
                Console.WriteLine($"QR Code value saved: {qrValue}");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", $"QR Code generated but failed to save: {ex.Message}", "OK");
            }
        }

        private async void OnAccountSettingsClicked(object sender, EventArgs e)
        {
            var accountSettingsPage = new AccountSettingsPage(database); // Pass the DatabaseHelper instance
            await Navigation.PushAsync(accountSettingsPage);
            SidebarOptions.IsVisible = false; // Hide sidebar after navigation
        }

        private async void OnContactUsClicked(object sender, EventArgs e)
        {
            var contactUsPage = new ContactUsPage(database); // Pass the DatabaseHelper instance
            await Navigation.PushAsync(contactUsPage);
            SidebarOptions.IsVisible = false; // Hide sidebar after navigation
        }


        // Handler for Logout button
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (confirm)
            {
                UserSession.Clear(); // Clear user session
                await Navigation.PopToRootAsync(); // Navigate back to the Login Page
            }
        }
        private void OnHamburgerClicked(object sender, EventArgs e)
        {
            SidebarOptions.IsVisible = !SidebarOptions.IsVisible;
        }
    }
}
