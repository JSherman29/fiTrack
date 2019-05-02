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
    public partial class RoutineFormPage : ContentPage
    {
        Routine routine;

        public RoutineFormPage(Routine routine)
        {
            InitializeComponent();
            this.routine = routine;
            this.Title = routine.Name;
        }

        private void AddExercise_Clicked(object sender, EventArgs e)
        {

        }

        private void SaveRoutine_Clicked(object sender, EventArgs e)
        {

        }
    }
}