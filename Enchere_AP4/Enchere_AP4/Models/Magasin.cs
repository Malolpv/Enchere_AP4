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
   public class Magasin
   {
        #region attributes 
        private int _id, _codePostal,_portable;
        private string _nom,_adresse,_ville;
        private double _latitude,_longitude;
        private ObservableCollection<Produit> _collProduits;

        public static ObservableCollection<Magasin> CollMagasins;
        #endregion
        #region constructor 

        public Magasin() 
        {
            _collProduits = new ObservableCollection<Produit>();
        }

        #endregion

        #region get/set
        
        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value;}
        [JsonProperty("nom")]
        public string Nom { get => _nom; set => _nom = value; }

        [JsonProperty("adresse")]
        public string Adresse { get => _adresse; set => _adresse = value; }

        [JsonProperty("ville")]
        public string Ville { get => _ville; set => _ville = value; }

        [JsonProperty("codepostal")]
        public int CodePostal { get => _codePostal; set => _codePostal = value; }

        [JsonProperty("portable")]
        public int Portable { get => _portable; set => _portable = value; }

        [JsonProperty("latitude")]
        public double Latitude { get => _latitude; set =>  _latitude = value; }

        [JsonProperty("longitude")]
        public double Longitude { get => _longitude; set => _longitude = value; }


        #endregion

        #region methods 
        public static async Task<ObservableCollection<Magasin>> GetMagasins()
        {
            return CollMagasins = await Tools.GetAllAsync<Magasin>("api/getMagasins");
        }


        #endregion
   }
}
