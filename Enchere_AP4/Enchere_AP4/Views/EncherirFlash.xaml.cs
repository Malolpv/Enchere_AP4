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
    public partial class EncherirFlash : ContentPage
    {

        EncherirFlashViewModel viewModel;

        private string _param = "";

        public string Param
        {
            get { return _param; }
            set { _param = Uri.UnescapeDataString(value ?? string.Empty); OnPropertyChanged(); }
        }


        public EncherirFlash()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            int.TryParse(Param, out int res);
            BindingContext = viewModel = new EncherirFlashViewModel(res);

            //on charge les boutons
            BuilButtonsFromTable(viewModel.LaEnchere.TableauFlash.Split(','));
        }


        private void Button_Clicked(object sender, EventArgs e)
        {
            sender.GetType().GetProperty("IsVisible").SetValue(sender, false);
        }

        /// <summary>
        /// charge les boutons à afficher/masquer selon le tableau des enchères flash
        /// </summary>
        /// <param name="param">array de type "string" avec l'etat de visibilité des enchères flash </param>
        private void BuilButtonsFromTable(string[] param)
        {
            int i = 0;
            foreach (object item in grid_Btn.Children)
            {
                item.GetType().GetProperty("IsVisible").SetValue(item, Convert.ToBoolean(Convert.ToInt32(param[i])));
                
                i++;
            }
            
        }
    }
}