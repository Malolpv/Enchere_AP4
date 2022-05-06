using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Enchere_AP4.Models
{
    public class Enchere
    {

        #region attributs
        private int _id;
        private DateTime _dateDebut;
        private DateTime _dateFin;
        private double _prixReserve;
        private string _tableauFlash;

        private Produit _produit;
        private Type_Enchere _leTypeEnchere;
        private Magasin _lemagasin;
        public static ObservableCollection<Enchere> CollEnchere = new ObservableCollection<Enchere>();

        #endregion

        #region get/set
        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }

        [JsonProperty("datedebut")]
        public DateTime DateDebut { get => _dateDebut; set => _dateDebut = value; }

        [JsonProperty("datefin")]
        public DateTime DateFin { get => _dateFin; set => _dateFin = value; }

        [JsonProperty("prixreserve")]
        public double PrixReserve { get => _prixReserve; set => _prixReserve = value; }

        [JsonProperty("leproduit")]
        public Produit LeProduit { get => _produit; set => _produit = value; }

        [JsonProperty("letypeenchere")]
        public Type_Enchere LeTypeEnchere { get => _leTypeEnchere; set => _leTypeEnchere = value; }

        [JsonProperty("lemagasin")]
        public Magasin LeMagasin { get => _lemagasin; set => _lemagasin = value; }
        
        [JsonProperty("tableauFlash")]
        public string TableauFlash { get => _tableauFlash; set => _tableauFlash = value; }

        #endregion

        #region constructeur
        public Enchere()
        {
            CollEnchere.Add(this);
        }
        

        public Enchere(int Id,double prixreserve,DateTime dateDebut,DateTime dateFin,Produit leproduit,Type_Enchere letypeenchere)
        {
            _id = Id;
            _prixReserve = prixreserve;
            _dateDebut = dateDebut;
            _dateFin = dateFin;
            
            CollEnchere.Add(this);
        }
        #endregion

        #region methodes 
        

        public static async Task<ObservableCollection<Enchere>> LoadEncheres()
        {
            return CollEnchere = await Tools.GetAllAsync<Enchere>("api/getEnchere");
        }

        public static Enchere getEnchereByID(int param)
        {
            foreach (Enchere enchere in CollEnchere)
            {
                if (enchere.Id == param)
                    return enchere;
            }
            return null;
        }
        #endregion
    }
}
