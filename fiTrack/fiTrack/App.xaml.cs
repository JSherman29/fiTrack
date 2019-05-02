using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using fiTrack.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace fiTrack
{
    public partial class App : Application
    {
        
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTcwMzNAMzEzNzJlMzEyZTMwTUNuUmRkNDJmV1VNby80S3gvMWJCMDVoUHpoNEs0SGtiTGxPQmtuQ2EwQT0=");
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            DataAccess.CreateTables();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
