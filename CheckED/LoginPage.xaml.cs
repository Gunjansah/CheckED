using System.Text.RegularExpressions;
namespace CheckED;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        Showpass.CheckedChanged += (s, args) =>
       {
           if (args.Value)
           {
               Loginpasword.IsPassword = false;
           }
           else
           {
               Loginpasword.IsPassword = true;
           }
       };

    }

    private void BtnForgotPassword(object sender, EventArgs e) // A simple page that asks for email and will have button send passoword
    {

    }

    private async void BtnCreateAcc(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAccount());
    }


    private async void BtnUserSignIn(object sender, EventArgs e)
    {
        if ((LoginEmail.Text == null) || (Loginpasword.Text == null))
        {
            await DisplayAlert("Error", "All Fields must be filled", "Ok");
        }
        else
        {
            if (IsValidEmail(LoginEmail.Text))
            {
                // Console.WriteLine($"Email: {pair.Key}, Password: {pair.Value}");
                if (emailPasswordDict.ContainsKey(LoginEmail.Text))
                {
                    if (emailPasswordDict[LoginEmail.Text] == Loginpasword.Text) // Just checking from dict rn
                    {
                        await DisplayAlert("Yahooo", "Yahooo", "Yahoo");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Error", "Your email or password is incorrect", "Ok");
                        LoginEmail.BackgroundColor = Color.Parse("#FFD5D2");
                        Loginpasword.BackgroundColor = Color.Parse("#FFD5D2");
                        return;
                    }
                }

                {
                    await DisplayAlert("Error", "Your email or password is incorrect", "Ok");
                    return;
                }
            }
            else
            {
                LoginEmail.BackgroundColor = Color.Parse("#FFD5D2");
                await DisplayAlert("Error", "Invalid Email Format", "OK");
            }

        }
    }

    public static bool IsValidEmail(string email)
    {
        // Regular expression pattern for validating an email
        string pattern = "@usm.edu";

        // Use Regex.IsMatch to check if the email matches the pattern
        return Regex.IsMatch(email, pattern);
    }

    Dictionary<string, string> emailPasswordDict = new Dictionary<string, string> // Sample Users can delete later
        {
            { "user1@example.com", "Password123!" },
            { "user2@example.com", "SecurePass456@" },
            { "test.user@example.com", "Test@789" },
            { "hello.world@example.org", "HelloWorld@2021" },
            { "john.doe@example.net", "JDoe#4567" }
        };


}
