using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using DotNetEnv;

namespace CheckED;

public partial class ResetPassword : ContentPage
{
    private string sendGridApiKey = "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";
    private string verificationCode;
    private string testCode = "2222";
    public ResetPassword()
	{
		InitializeComponent();
     
    }

    
    private async void SendVerificationCode(object sender, EventArgs e)
    {
        

        string userEmail = VerificationEmailInput.Text; 
        if (string.IsNullOrEmpty(userEmail))
        {
            await DisplayAlert("Error", "Please enter a valid email address.", "OK");
            return;
        }

        var verificationCode = new Random().Next(100000, 999999).ToString();

        bool emailSent = await SendVerificationEmail(userEmail, verificationCode);

        if (emailSent)
        {
            await DisplayAlert("Success", "Verification code sent to your email.", "OK");
            VerificationEmailInput.IsVisible = false;
            VerificationEmailLabel.IsVisible = false;
            

            SendVerificationCoded.IsVisible = false;

            VerificationCodeInput.IsVisible = true;
            VerificationCodeLabel.IsVisible = true;
            VerificationCodeFrame.IsVisible = true;

            ConfirmVerificationCoded.IsVisible = true;


        }
        else
        {
            await DisplayAlert("Error", "Failed to send verification code. Please try again.", "OK");
        }
    }

   


        
        private async Task<bool> SendVerificationEmail(string userEmail, string verificationCode)
        {
            var client = new SendGridClient(sendGridApiKey);
            var from = new EmailAddress("c9ivira@gmail.com", "CheckED Support");
            var subject = "Your Verification Code";
            var to = new EmailAddress(userEmail);
            var plainTextContent = $"Your verification code is: {verificationCode}";
            var htmlContent = $"<strong>Your verification code is: {verificationCode}</strong>";
            var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(message);

            return response.IsSuccessStatusCode;
        }
        

        private async void ReturntoLogin(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

  
    private void ConfirmVerificationCode(object sender, EventArgs e)
    {
      
        string userInputCode = VerificationCodeInput.Text;

        if (string.IsNullOrEmpty(userInputCode))
        {
            DisplayAlert("Error", "Please enter the verification code.", "OK");
            return;
        }

      
        if (userInputCode == testCode) //VerificationCode
        {
          
            DisplayAlert("Success", "Verification code is correct!", "OK");


            VerificationCodeInput.IsVisible = false;
            VerificationCodeLabel.IsVisible = false;
            VerificationCodeFrame.IsVisible = false;

            ConfirmVerificationCoded.IsVisible = false;

            PasswordField.IsVisible = true;
            ConfirmPasswordField.IsVisible = true;
            PasswordBtn.IsVisible = true;


        }
        else
        {
           
            DisplayAlert("Error", "Verification code is incorrect. Please try again.", "OK");
        }
    }

    private void PasswordBtn_Clicked(object sender, EventArgs e)
    {

    }
}
