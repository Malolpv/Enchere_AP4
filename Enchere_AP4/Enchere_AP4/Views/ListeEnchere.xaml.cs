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
    public partial class ListeEnchere : ContentPage
    {
        ListeEnchereViewModel viewModel;
        public ListeEnchere()
        {
            InitializeComponent();
            BindingContext = viewModel = new ListeEnchereViewModel();
        }

        private void InverseCouleurFiltres(Button param)
        {
            //rend le bouton sélectionné 
            SelectedFilterUI(param);

            switch(param.Text)
            {
                case "Aucun":
                    ResetFilterUI(btn_Classique);
                    ResetFilterUI(btn_Inverse);
                    ResetFilterUI(btn_Flash);
                    break;
                
                case "Classique":
                    ResetFilterUI(btn_Aucun);
                    ResetFilterUI(btn_Inverse);
                    ResetFilterUI(btn_Flash);
                    break;

                case "Inversé":
                    ResetFilterUI(btn_Aucun);
                    ResetFilterUI(btn_Classique);
                    ResetFilterUI(btn_Flash);
                    break;

                case "Flash":
                    ResetFilterUI(btn_Aucun);
                    ResetFilterUI(btn_Inverse);
                    ResetFilterUI(btn_Classique);
                    break;
            }
        }
        
        private void ResetFilterUI(Button param)
        {
            param.BackgroundColor = Color.White;
            param.TextColor= Color.FromHex("#1C375C");
            param.BorderColor= Color.FromHex("#1C375C");
        }

        private void SelectedFilterUI(Button param)
        {
            param.BackgroundColor = Color.FromHex("#1C375C");
            param.TextColor = Color.White;
            param.BorderColor = Color.White;

        }

        private void btn_Aucun_Clicked(object sender, EventArgs e)
        {
            InverseCouleurFiltres(btn_Aucun);
        }

        private void btn_Classique_Clicked(object sender, EventArgs e)
        {
            InverseCouleurFiltres(btn_Classique);
        }

        private void btn_Inverse_Clicked(object sender, EventArgs e)
        {
            InverseCouleurFiltres(btn_Inverse);
        }

        private void btn_Flash_Clicked(object sender, EventArgs e)
        {
            InverseCouleurFiltres(btn_Flash);
        }
    }
}