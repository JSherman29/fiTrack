using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fiTrack.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoutinesPage : ContentPage
    {
        public RoutinesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            UpdateUI();
        }

        private void UpdateUI()
        {
            RoutineGrid.ItemsSource = DataAccess.GetRoutines();
            lblNoData.IsVisible = ((List<Routine>)RoutineGrid.ItemsSource).Count == 0;
            RoutineGrid.IsVisible = !lblNoData.IsVisible;
        }

        private void NewRoutine_Clicked(object sender, EventArgs e)
        {
            AddRoutineLayout.IsVisible = true;
        }

        private async void AddRoutine_Clicked(object sender, EventArgs e)
        {
            Routine routine = new Routine();
            routine.Name = NameEntry.Text;
            DataAccess.SaveRoutine(routine);
            AddRoutineLayout.IsVisible = false;
            UpdateUI();
            await DisplayAlert("", $"{routine.Name} added.", "Ok");
        }

        private async void RoutineGrid_GridLongPressed(object sender, Syncfusion.SfDataGrid.XForms.GridLongPressedEventArgs e)
        {
            Routine routine = (Routine)e.RowData;
            string action = await DisplayActionSheet("", "Cancel", null, "Edit", "Delete");

            if (action == "Edit")
            {
                await Navigation.PushModalAsync(new NavigationPage(new RoutineFormPage(routine)));
            }
            else if (action == "Delete")
            {
                bool delete = await DisplayAlert("", $"Are you sure you want to permanently delete {routine.Name}?", "Yes", "No");

                if (delete)
                {
                    DataAccess.DeleteRoutine(routine);
                    UpdateUI();
                }
            }
        }
    }
}