using System.Text;
using Microsoft.Maui.Controls;
using System;
using System.Security.Cryptography;


namespace CheckED;

public partial class LoginPageCreator : ContentPage
{
    DatabaseHelper database;
    public LoginPageCreator()
    {
        InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Users.db3");
        database = new DatabaseHelper(dbPath);
    }

    async void BtnUserSignIn(object sender, EventArgs e)
    {
        var user = await database.GetUserAsync(UserEmailLogin.Text);

        if (user == null || user.Password != ComputeHash(UserPasswordLogin.Text))
        {
            await DisplayAlert("Error", "Invalid login credentials", "OK");
            return;
        }

        UserSession.UserId = user.Id;

        await Navigation.PushAsync(new CreatorDashboard());
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


    private async void BtnCreateAcc(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAccount());
    }

    private async void BtnForgotPassword(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResetPassword());
    }
}
