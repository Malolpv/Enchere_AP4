using Enchere_AP4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Enchere_AP4.ViewModels
{
    public class MagasinMapViewModel : BaseViewModel
    {

        #region attributes
        private ObservableCollection<Magasin> _lesMagasins;

        private ObservableCollection<Pin> _lesPinsMagasin;
        #endregion

        #region constructor
        public MagasinMapViewModel()
        {
            LoadMagasins();

            
        }

        #endregion

        #region get/set
        public ObservableCollection<Magasin> LesMagasins
        {
            get => _lesMagasins;
            set => SetProperty(ref _lesMagasins, value);
        }

        public ObservableCollection<Pin> LesPinsMagasins
        {
            get => _lesPinsMagasin;
            set => SetProperty(ref _lesPinsMagasin, value);
        }
        #endregion

        #region methodes

        // Task<ObservableCollection<Magasin>>
        private async void LoadMagasins()
        {
            //LesMagasins = Magasin.CollMagasins.Count > 0 || Magasin.CollMagasins != null ? Magasin.CollMagasins : await Magasin.GetMagasins();
            
            LesMagasins = await Magasin.GetMagasins();
            if(LesMagasins != null)
                LesPinsMagasins = LoadPinsMagasins(LesMagasins);
        }





        private ObservableCollection<Pin> LoadPinsMagasins(ObservableCollection<Magasin> param)
        {
            
            ObservableCollection<Pin> res = new ObservableCollection<Pin>();
            foreach (Magasin magasin in param)
            {
                res.Add(new Pin()
                {
                    Address = magasin.Adresse + " " + magasin.CodePostal + " " + magasin.Ville,
                    Label = magasin.Nom,
                    Position = new Position(magasin.Latitude, magasin.Longitude)
                });

            }
            return res;
        }



        
        #endregion

    }
}
