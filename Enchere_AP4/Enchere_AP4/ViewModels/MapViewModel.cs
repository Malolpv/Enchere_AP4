using Enchere_AP4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace Enchere_AP4.ViewModels
{
    
    public class MapViewModel : BaseViewModel
    {
        private ObservableCollection<Pin> _pins;
        public MapViewModel(int idEnchere)
        {
            _pins = new ObservableCollection<Pin>();
            Enchere e = Enchere.getEnchereByID(idEnchere);
            
            //création du pin du magasin
            Pins.Add(new Pin() 
            { 
                Address = e.LeMagasin.Adresse + " " + e.LeMagasin.CodePostal + " " + e.LeMagasin.Ville,
                Label = e.LeMagasin.Nom,Type = PinType.SearchResult, 
                Position = new Position(e.LeMagasin.Latitude,e.LeMagasin.Longitude)
            }) ;
            
            /* désactivé car IsShowingUser est à vrai dans MapView.xaml
            //pin du user
            App.LoggedUser.GetLocation();
            Pins.Add(new Pin()
            {
                Label = "Votre position",Type = PinType.SearchResult, Position = App.LoggedUser.UserPosition
            }
            );*/

        }

            
        #region get/set
        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        
        #endregion
    }
}
