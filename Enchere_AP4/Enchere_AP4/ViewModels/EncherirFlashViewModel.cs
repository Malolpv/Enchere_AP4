using Enchere_AP4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Enchere_AP4.ViewModels
{
    public class EncherirFlashViewModel : BaseViewModel
    {


        #region attributs

        private Enchere _laEnchere;


        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param">id de l'enchère à charger</param>
        public EncherirFlashViewModel(int param)
        {
            LaEnchere = Enchere.getEnchereByID(param);
        }


        #endregion

        #region get/set

        public Enchere LaEnchere
        {
            get => _laEnchere;
            set => SetProperty(ref _laEnchere,value);
        }

        
        #endregion

        #region methods
        




        #endregion 
    }
}
