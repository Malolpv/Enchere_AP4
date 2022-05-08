using Enchere_AP4.Models;
using Enchere_AP4.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enchere_AP4.ViewModels
{
    public class ListeEnchereViewModel : BaseViewModel
    {

        #region attributs
        public static ObservableCollection<string> Filtres = new ObservableCollection<string>() { "aucun","classique","inverse","flash"};
        
        
        private ObservableCollection<Enchere> _lesEncheres;
        private Enchere _selectedEnchere;

        private Dictionary<string, ObservableCollection<Enchere>> _dicoEnchereFiltre; 
        #endregion

        #region get/set
        public ObservableCollection<Enchere> LesEncheres
        {
            get => _lesEncheres;    set => SetProperty(ref _lesEncheres, value);
        }

        public Enchere SelectedEnchere
        {
            get => _selectedEnchere;
            set => SetProperty(ref _selectedEnchere, value);
        }

        public ICommand PageEnchereInfo { get; }
        
        public ICommand AucunCommand { get; }

        public ICommand ClassiqueCommand { get; }

        public ICommand InverseCommand { get; }

        public ICommand FlashCommand { get; }

        #endregion

        #region constructeur
        public ListeEnchereViewModel()
        {
            Title = "Les Enchères";

            PageEnchereInfo = new Command(() => InfoEnchere());
            AucunCommand = new Command(() => LesEncheres = GetEnchereEnCours());
            ClassiqueCommand = new Command(() => DisplayEnchereByFilter("classique"));
            InverseCommand = new Command(() => DisplayEnchereByFilter("inverse"));
            FlashCommand = new Command(() => DisplayEnchereByFilter("flash"));

            LesEncheres = new ObservableCollection<Enchere>();
            _dicoEnchereFiltre = new Dictionary<string,ObservableCollection<Enchere>>();
            GetEncheres();

        }

        
        #endregion

        #region methodes
        

        private async void InfoEnchere()
        {
            var route = $"{nameof(EnchereInfo)}?Param={SelectedEnchere.Id}";
            await Shell.Current.GoToAsync(route);
        }
        
        private async void GetEncheres()
        {

            _ = Enchere.CollEnchere.Count > 0 ? Enchere.CollEnchere : await Enchere.LoadEncheres();
            if(Enchere.CollEnchere != null)
            {
                LesEncheres = GetEnchereEnCours();
                if(LesEncheres != null)
                    FiltreEnchere(LesEncheres);

            }
            else
            {
                Tools.ShowLongToast("Aucune enchère n'est actuellement en cours");
            }

        }

        private void PrepareDico()
        {
            _dicoEnchereFiltre.Clear();
            _dicoEnchereFiltre.Add("inverse", new ObservableCollection<Enchere>());
            _dicoEnchereFiltre.Add("classique", new ObservableCollection<Enchere>());
            _dicoEnchereFiltre.Add("flash", new ObservableCollection<Enchere>());
            
        }

        /// <summary>
        /// TODO 
        /// vérifier les filtres avec le truc enchere inversevrai
        /// permet de constituer le dictionnaire d'enchères filtrées
        /// ignore le type enchere "inversevrai"
        /// </summary>
        /// <param name="param"></param>
        private void FiltreEnchere(ObservableCollection<Enchere> param)
        {
            PrepareDico();
            foreach(Enchere e in param)
            {
                if(e.LeTypeEnchere.Nom != "inversevrai")
                    _dicoEnchereFiltre[e.LeTypeEnchere.Nom].Add(e);
            }

        }

        private void DisplayEnchereByFilter(string param)
        {
            ObservableCollection<Enchere> res;
            if (_dicoEnchereFiltre.TryGetValue(param, out res))
                LesEncheres = res;
        }

        public ObservableCollection<Enchere> GetEnchereEnCours()
        {
            ObservableCollection<Enchere> res = new ObservableCollection<Enchere>();
            foreach (Enchere e in Enchere.CollEnchere)
            {
                if (e.DateFin > DateTime.Now && e.DateDebut <= DateTime.Now)
                    res.Add(e);
            }
            return res;
        }

        #endregion
    }
}
