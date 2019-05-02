using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fiTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ConfigPage()));
        }
    }
}