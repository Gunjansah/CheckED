using Microsoft.Maui.Controls;
using System;
using System.Security.Cryptography;
using System.Text;
using CheckED;
namespace CheckED;


    public partial class CreateAccount : ContentPage
    {
        DatabaseHelper database;

        public CreateAccount()
        {
            InitializeComponent();
            
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Users.db3");

            database = new DatabaseHelper(dbPath);
            Console.WriteLine(dbPath);
            Console.WriteLine($"Database Path: {dbPath}");
        }

        async void BtnCreateAccount(object sender, EventArgs e)
        {
        // Validate inputs
            if (string.IsNullOrWhiteSpace(UserNameCA.Text) ||
                string.IsNullOrWhiteSpace(UserEmailCA.Text) ||
                string.IsNullOrWhiteSpace(UserPhoneCA.Text) ||
                string.IsNullOrWhiteSpace(UserPasswordCA.Text) ||
                string.IsNullOrWhiteSpace(UserConfirmPasswordCA.Text))
            {
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

        if (UserPasswordCA.Text != UserConfirmPasswordCA.Text)
            {
                await DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            string hashedPassword = ComputeHash(UserPasswordCA.Text);

            User user = new User
            {
                Name = UserNameCA.Text,
                Email = UserEmailCA.Text,
                Phone = UserPhoneCA.Text,
                Password = hashedPassword 
            };

            await database.SaveUserAsync(user);
            await DisplayAlert("Success", "Account created successfully", "OK");
            await Navigation.PopAsync(); 
        }

        private string ComputeHash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }


