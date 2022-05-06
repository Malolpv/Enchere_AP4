using Enchere_AP4.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Enchere_AP4.ViewModels
{
    public class AccueilViewModel : BaseViewModel
    {
        #region attributes

        private User _leUser;
        #endregion

        #region Constructor
        public AccueilViewModel(User param)
        {
            LeUser = param;
        }

        #endregion

        #region get/set

        public User LeUser
        {
            get => _leUser;
            set => SetProperty(ref _leUser, value);
        }
        
        #endregion

        #region methodes

        
        
        #endregion



    }
}