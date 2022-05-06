using Enchere_AP4.Models;
using Enchere_AP4.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enchere_AP4.ViewModels
{
    public class EnchereInfoViewModel : BaseViewModel
    {
        #region attributs
        private Enchere _laEnchere;
        

        #endregion


        #region get/set
        public Enchere LaEnchere { get => _laEnchere; set => SetProperty(ref _laEnchere, value);}

        public ICommand GoToEncherirCommand { get; }
        public ICommand GoToMapViewCommand { get; }
        #endregion

        #region Constructeur
        public EnchereInfoViewModel(int idEnchere)
        {
            LaEnchere = Enchere.getEnchereByID(idEnchere);
            GoToEncherirCommand = new Command(() => GoToEncherir());
            GoToMapViewCommand = new Command(() => GoToMapView());
        }

        #endregion

        

        #region methode

        /// <summary>
        /// renvoie l'utilisateur vers la page correspondante au type de l'enchère sélectionnée 
        /// </summary>
        private async void GoToEncherir()
        {
            
            string route = "";
            switch(LaEnchere.LeTypeEnchere.Nom)
            {
                case "classique":
                    route = $"{nameof(Views.Encherir)}?Param={LaEnchere.Id}";
                    break;

                case "inverse":
                    route = $"{nameof(Views.EncherirInverse)}?Param={LaEnchere.Id}";
                    break;

                case "flash":
                    route = $"{nameof(Views.EncherirFlash)}?Param={LaEnchere.Id}";
                    break;

                default:
                    break;
            }
            if(route != "")
                await Shell.Current.GoToAsync(route);

        }

        private async void GoToMapView()
        {
            var route = $"{nameof(MapView)}?Param={LaEnchere.Id}";
            await Shell.Current.GoToAsync(route);
        }
        #endregion

    }
}
