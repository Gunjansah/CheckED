using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace CheckED
{
    public partial class ContactUsPage : ContentPage
    {
        private readonly DatabaseHelper _database;

        public ContactUsPage(DatabaseHelper database)
        {
            InitializeComponent();
            _database = database;
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            string name = UserNameEntry.Text?.Trim();
            string email = UserEmailEntry.Text?.Trim();
            string message = UserMessageEditor.Text?.Trim();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(message))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            if (!IsValidEmail(email))
            {
                await DisplayAlert("Error", "Please enter a valid email address.", "OK");
                return;
            }

            // Optionally, save the message to the database
            var contactMessage = new ContactMessage
            {
                Name = name,
                Email = email,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            try
            {
                await _database.SaveContactMessageAsync(contactMessage);
                await DisplayAlert("Success", "Your message has been sent successfully.", "OK");
                // Optionally, navigate back or clear the form
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to send your message: {ex.Message}", "OK");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }
    }
}
