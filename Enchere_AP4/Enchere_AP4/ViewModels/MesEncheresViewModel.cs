using Enchere_AP4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;

namespace Enchere_AP4.ViewModels
{
    public class MesEncheresViewModel : BaseViewModel
    {


        #region attributes

        private ObservableCollection<Enchere> _lesEncheres;
        private Dictionary<Enchere,Color> _dicoEncheres;

        #endregion


        #region constructor
        
        public MesEncheresViewModel()
        {
            DicoEncheres = new Dictionary<Enchere, Color>();
            LoadEncheresParticipees();
        }

        #endregion


        #region get/set

        public ObservableCollection<Enchere> LesEncheres
        {
            get => _lesEncheres;
            set => SetProperty(ref _lesEncheres, value);
        }

        public Dictionary<Enchere, Color> DicoEncheres
        {
            get => _dicoEncheres;
            set => SetProperty(ref _dicoEncheres, value);
        }

        #endregion


        #region methodes

        private async void LoadEncheresParticipees()
        {
            
            LesEncheres = await Tools.PostAsyncMultyParam<ObservableCollection<Enchere>>("api/getEncheresParticipes",new Dictionary<string, string>() { { "Id",App.LoggedUser.Id.ToString()} });

            foreach(Enchere e in LesEncheres)
            {
                
                    if (e.Id%2 == 0)
                    {
                        DicoEncheres.Add(e,Color.Green);
                    }
                    else
                        DicoEncheres.Add(e,Color.Red);
                
            }
        }
        #endregion

    }
}
