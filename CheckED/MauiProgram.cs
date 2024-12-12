using Microsoft.Extensions.Logging;

namespace CheckED
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register DatabaseHelper as a singleton
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Users.db3");
            builder.Services.AddSingleton(new DatabaseHelper(dbPath));

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<UserDashboard>();
            builder.Services.AddTransient<AccountSettingsPage>();
            builder.Services.AddTransient<ContactUsPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
