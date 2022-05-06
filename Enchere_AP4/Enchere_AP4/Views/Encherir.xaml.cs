using Enchere_AP4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Enchere_AP4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(Param),nameof(Param))]
    public partial class Encherir : ContentPage
    {
        private string _param ="";

        public string Param
        {
            get { return _param; }
            set { _param = Uri.UnescapeDataString(value ?? string.Empty); OnPropertyChanged(); }
        }

        EncherirViewModel ViewModel;

        public Encherir()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(Param, out int res);
            BindingContext = ViewModel = new EncherirViewModel(res);
            
        }

        private void entryMontant_Focused(object sender, FocusEventArgs e)
        {
            frameEntry.BorderColor = Color.DarkRed;
        }

        private void entryMontant_Unfocused(object sender, FocusEventArgs e)
        {
            frameEntry.BorderColor= Color.LightGray;
        }

        private void btn_Help_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Aide", "Cette enchère est une enchère classique.\n Pour enchérir il vous suffit d'entrer un montant supérieur au montant actuel dans la zone de saisie ci-dessous.","J'ai compris");
        }

        private void btn_Encherir_Clicked(object sender, EventArgs e)
        {
            entryMontant.Text = "";
        }


    }
}