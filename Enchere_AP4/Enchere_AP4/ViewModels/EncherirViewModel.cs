using Enchere_AP4.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enchere_AP4.ViewModels
{
    public class EncherirViewModel : BaseViewModel
    {

        #region attributs

        private Enchere _laEnchere;
        private Encherir _lEncherir;
        private string _tempsRestant;
        private double _prixMinimum;

        private bool _entryVisible;
        private bool _winVisible;
        #endregion

        #region Constructeur

        public EncherirViewModel(int idEnchere)
        {

            LaEnchere = Enchere.getEnchereByID(idEnchere);

            EncherirCommand = new Command<string>((x) => Encherir(Convert.ToDouble(x)));

            //on charge une foix le prix à l'affichage
            GetActualPrice();
            //on relance la routine avec cette fois ci un délai de en secondes entre chaque appel
            GetActualPriceLoop(true, 15);
        }

        #endregion

        #region get/set
        public Enchere LaEnchere { get { return _laEnchere;} set { SetProperty(ref _laEnchere, value); } }
        public Encherir LEncherir { get { return _lEncherir;} set {SetProperty(ref _lEncherir, value); } }

        public string GetTempsRestant
        {
            get {  TimeSpan t = (LaEnchere.DateFin - DateTime.Now) ;
                return "Il reste " + t.Days + " jours, " + t.Hours + " heures et " + t.Minutes + " minutes";
            }

            set { SetProperty(ref _tempsRestant, value);}
        }


        public double ProgressBarValue
        {
            get { return (DateTime.Now - LaEnchere.DateDebut).TotalHours / (LaEnchere.DateFin - LaEnchere.DateDebut).TotalHours; }
        }


        public double PrixMinimum
        {
            get => _prixMinimum;
            set => SetProperty(ref _prixMinimum, value); 
        }

        public ICommand EncherirCommand { get; }

        public bool EntryVisible
        {
            get => _entryVisible;
            set => SetProperty(ref _entryVisible,value);
        }

        public bool WinVisible
        {
            get => _winVisible;
            set => SetProperty(ref _winVisible,value);
        }
        #endregion

        #region methodes

        /// <summary>
        /// s'execute en arrière plan pour rafraichir le prix de l'enchère
        /// </summary>
        /// <param name="loopBack"> vrai => le chrono redémarre à chaque fois qu'il est terminé || faux => le chrono s'arrête à la fin du premier passage</param>
        /// <param name="delay">le délai entre chaque déclenchement</param>
        private void GetActualPriceLoop(bool loopBack,int delay)
        {
            Device.StartTimer(TimeSpan.FromSeconds(delay), () =>
            {
                Task.Run(async () =>
                {
                    LEncherir = await Tools.GetOneAsync<Encherir>("api/getActualPrice", "Id", LaEnchere.Id);
                    if (LEncherir == null)
                        LEncherir = new Encherir() { PrixEnchere = LaEnchere.PrixReserve };
                    else
                    {
                        LEncherir.Id = 0;
                        if (LEncherir.LeUser.Id == App.LoggedUser.Id)
                        {
                            EntryVisible = false; 
                            WinVisible = true;
                        }
                        else
                        {
                            EntryVisible = true;
                            WinVisible = false;
                        }
                    }
                    PrixMinimum = LEncherir.PrixEnchere;
                });
                return loopBack;
            });
        }

        /// <summary>
        /// renvoie le prix actuel de l'enchère en cours
        /// </summary>
        private async void GetActualPrice()
        {
            LEncherir = await Tools.GetOneAsync<Encherir>("api/getActualPrice", "Id", LaEnchere.Id);
            if (LEncherir == null)
                LEncherir = new Encherir() { PrixEnchere = LaEnchere.PrixReserve };
            else
            {
                LEncherir.Id = 0;
                if(LEncherir.LeUser.Id == App.LoggedUser.Id)
                {
                    EntryVisible = false; 
                    WinVisible = true;
                }
                else
                {
                    EntryVisible = true;
                    WinVisible = false;
                }
            }
            PrixMinimum = LEncherir.PrixEnchere;
        }

        private void Encherir(double param)
        {
            switch (LaEnchere.LeTypeEnchere.Nom)
            {
                case "inverse":
                    EncherirInverse(param);
                    break;

                case "classique":
                    EncherirClassique(param);
                    break;

                case "flash":

                    break;
                    
            }

            GetActualPrice();
        }


        /// <summary>
        /// envoie un objet enchérir inverse dans la bdd
        /// </summary>
        /// <param name="param"></param>
        private async void EncherirInverse(double param)
        {
            
            if(0 < param && param < PrixMinimum)
            {
                Dictionary<string, string> dicoParam = new Dictionary<string, string>();

                dicoParam.Add("IdUser", App.LoggedUser.Id.ToString());
                dicoParam.Add("IdEnchere", LaEnchere.Id.ToString());
                dicoParam.Add("PrixEnchere", param.ToString());
                await Tools.PostAsyncMultyParam("api/postEncherir", dicoParam);
                    
            }
            else
            {
                if (param <= 0)
                    Tools.ShowShortToast("L'enchère doit être supérieure à 0 ");
                else
                    Tools.ShowShortToast("L'enchèredoit infèrieure au prix maximum");
            }

        }

        /// <summary>
        /// envoie un objet enchérir classique dans la bdd
        /// </summary>
        /// <param name="param"></param>
        private async void EncherirClassique(double param)
        {
            if (param > PrixMinimum && App.LoggedUser.Id != LEncherir.LeUser.Id)
            {
                Dictionary<string, string> dicoParam = new Dictionary<string, string>();

                dicoParam.Add("IdUser", App.LoggedUser.Id.ToString());
                dicoParam.Add("IdEnchere", LaEnchere.Id.ToString());
                dicoParam.Add("PrixEnchere", param.ToString());
                await Tools.PostAsyncMultyParam("api/postEncherir", dicoParam);
                
            }
            else
            {
                Tools.ShowShortToast("L'enchère doit être supérieure au prix minimum");

            }

        }


        
        #endregion

    }
}
