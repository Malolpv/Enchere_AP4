using Enchere_AP4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Enchere_AP4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Param), nameof(Param))]
    public partial class EncherirInverse : ContentPage
    {

        EncherirInverseViewModel viewModel;

        private string _param = "";

        public string Param
        {
            get { return _param; }
            set { _param = Uri.UnescapeDataString(value ?? string.Empty); OnPropertyChanged(); }
        }


        public EncherirInverse()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(Param, out int res);
            //Ajouter le vue modele
            BindingContext = viewModel = new EncherirInverseViewModel(res);

            SetProgress();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //permet d'arreter la task en cours
            viewModel.LoopBack = false;
        }

        private async void EncherirInverseRoutine(int delay,bool loopBack)
        {
            Device.StartTimer(TimeSpan.FromSeconds(delay), () =>
            {
                Task.Run(async () => SetProgress());
                return loopBack;
            });
            
            
        }

        /// <summary>
        /// set progress bar value
        /// </summary>
        private void SetProgress()
        {
            progressBar.Progress = (DateTime.Now - viewModel.LaEnchere.DateDebut).TotalHours / (viewModel.LaEnchere.DateFin - viewModel.LaEnchere.DateDebut).TotalHours;
        }

        private void btn_Help_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Aide", "Cette enchère est une enchère inverse.\nPour enchérir il vous suffit d'entrer un montant infèrieur au montant actuel dans la zone de saisie ci-dessous." +
                "\nA la fin du temps imparti, si le montant de l'enchère est infèrieur à son prix de réserve, la vente se verra alors annulée.", "J'ai compris");
        }

        private void btn_Encherir_Clicked(object sender, EventArgs e)
        {
            entryMontant.Text = "";
        }

        private void entryMontant_Focused(object sender, FocusEventArgs e)
        {
            frameEntry.BorderColor = Color.DarkRed;
        }

        private void entryMontant_Unfocused(object sender, FocusEventArgs e)
        {
            frameEntry.BorderColor = Color.LightGray;
        }
    }
}