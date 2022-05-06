//Generated by Generator
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Enchere_AP4.Models
{
   public class Type_Enchere
   {
        #region attributes 
        private int _id;
        private string _nom;
        private ObservableCollection<Enchere> _collEnchere;
        #endregion
        #region constructor 

        public Type_Enchere() 
        {
            _collEnchere = new ObservableCollection<Enchere>();
        }

        #endregion
        #region get/set 
        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }
        [JsonProperty("nom")]
        public string Nom { get => _nom; set => _nom = value; }

        #endregion
        #region methods 

        #endregion
   }
}