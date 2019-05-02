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
    public partial class ExercisePage : ContentPage
    {
        public string Name { get; set; }

        public ExercisePage()
        {
            InitializeComponent();
            ExerciseGrid.GroupCaptionTextFormat = "{Key}";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ExerciseGrid.ItemsSource = DataAccess.GetExercises();

            lblNoData.IsVisible = ((List<Exercise>)ExerciseGrid.ItemsSource).Count == 0;
            ExerciseGrid.IsVisible = !lblNoData.IsVisible;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ExerciseFormPage()), true);
        }

        private async void ExerciseGrid_GridLongPressed(object sender, Syncfusion.SfDataGrid.XForms.GridLongPressedEventArgs e)
        {
            Exercise exercise = (Exercise)e.RowData;
            string action = await DisplayActionSheet("", "Cancel", null, "Edit", "Delete");

            if (action == "Edit")
            {
                await Navigation.PushModalAsync(new NavigationPage(new ExerciseFormPage(exercise)));
            }
            else if (action == "Delete")
            {
                bool delete = await DisplayAlert("", $"Are you sure you want to permanently delete {exercise.Name}?", "Yes", "No");

                if (delete)
                {
                    DataAccess.DeleteExercise(exercise.Id);
                    ExerciseGrid.ItemsSource = DataAccess.GetExercises();
                }
            }
        }
    }
}