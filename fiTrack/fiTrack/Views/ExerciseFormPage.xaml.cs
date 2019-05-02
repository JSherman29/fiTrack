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
    public partial class ExerciseFormPage : ContentPage
    {
        private Exercise exercise;
        private bool isNew;

        public ExerciseFormPage()
        {
            InitializeComponent();
            exercise = new Exercise();
            isNew = true;
            this.Title = "New Exercise";
            Save.Text = "Save";
        }

        public ExerciseFormPage(Exercise exercise)
        {
            InitializeComponent();

            NameEntry.Text = exercise.Name;
            MuscleGroupsPicker.SelectedItem = exercise.PrimaryMuscle;
            WeightCheck.IsChecked = exercise.HasWeight;
            RepsCheck.IsChecked = exercise.HasReps;
            TimeCheck.IsChecked = exercise.HasTime;
            DistanceCheck.IsChecked = exercise.HasDistance;

            this.exercise = exercise;
            isNew = false;
            this.Title = "Edit Exercise";
            Save.Text = "Update";
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            exercise.Name = NameEntry.Text;
            exercise.PrimaryMuscle = MuscleGroupsPicker.SelectedItem.ToString();
            exercise.HasWeight = (bool)WeightCheck.IsChecked;
            exercise.HasReps = (bool)RepsCheck.IsChecked;
            exercise.HasDistance = (bool)DistanceCheck.IsChecked;
            exercise.HasTime = (bool)TimeCheck.IsChecked;

            if (isNew)
            {
                DataAccess.SaveExercise(exercise);
                await DisplayAlert("", $"{exercise.Name} has been saved to the database.", "Ok");
            }
            else
            {
                DataAccess.UpdateExercise(exercise);
                await DisplayAlert("", $"{exercise.Name} has been updated.", "Ok");
            }

            await Navigation.PopModalAsync();
        }
    }
}