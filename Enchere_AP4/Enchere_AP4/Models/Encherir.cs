using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Enchere_AP4.Models
{
    public class Encherir
    {

        #region attributs
        private User _leUser;
        private Enchere _laEnchere;

        
        private double _prixEnchere;
        private DateTime _dateEnchere;

        #endregion

        #region constructeur
        public Encherir()
        {
            LeUser = new User();
        }

        public Encherir(double prix,User user,Enchere enchere)
        {
            PrixEnchere = prix;
            DateEnchere = DateTime.Now;
            LeUser = user;
            LaEnchere = enchere;

        }

        #endregion

        #region get/set

        public double PrixEnchere
        {
            get => _prixEnchere;
            set => _prixEnchere = value;
        }

        public int Id
        {
            get => LeUser.Id;
            set => LeUser.Id = value; 
        }
        
        public DateTime DateEnchere
        {
            get => _dateEnchere;
            set => _dateEnchere = value;
        }
        public User LeUser 
        { 
            get => _leUser; 
            set => _leUser = value; 
        }
        public Enchere LaEnchere 
        { 
            get => _laEnchere; 
            set => _laEnchere = value; 
        }

        

        #endregion

        #region methodes

        

        #endregion
    }
}
