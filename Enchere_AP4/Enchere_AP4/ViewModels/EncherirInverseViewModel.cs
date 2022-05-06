using Enchere_AP4.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Enchere_AP4.ViewModels
{
    public class EncherirInverseViewModel : BaseViewModel
    {


        #region attributes

        private Enchere _laEnchere;
        private Encherir _lEncherir;
        private double _prixMaximum;
        private bool _entryVisible;
        private bool _winVisible;
        #endregion

        #region constructor
        /// <summary>
        /// initialise l'objet vue modele et récupère l'enchère en cours
        /// </summary>
        /// <param name="id">id de l'enchère à charger</param>
        public EncherirInverseViewModel(int id)
        {
            LaEnchere = Enchere.getEnchereByID(id);
            EncherirCommand = new Command<string>((x) => EncherirInverse(Convert.ToDouble(x)));
            GetActualPrice();
            GetActualPriceLoop(15);
        }
        #endregion

        #region get/set
        public Enchere LaEnchere 
        {
            get => _laEnchere;
            set => SetProperty(ref _laEnchere, value);
        }

        public Encherir LEncherir
        {
            get => _lEncherir;
            set => SetProperty(ref _lEncherir, value);
        }

        public double PrixMaximum
        {
            get => _prixMaximum;
            set => SetProperty(ref _prixMaximum, value);
        }

        public ICommand EncherirCommand { get; }

        public bool LoopBack = true;


        public bool EntryVisible
        {
            get => _entryVisible;
            set => SetProperty(ref _entryVisible, value);
        }

        public bool WinVisible
        {
            get => _winVisible;
            set => SetProperty(ref _winVisible, value);
        }
        #endregion

        #region methodes

        /// <summary>
        /// enchéri sur une enchère inversée
        /// </summary>
        /// <param name="param">montant de la nouvelle enchère, qui doit être infèrieur au montant précédent</param>
        private async void EncherirInverse(double param)
        {

            if (0 < param && param < _prixMaximum)
            {
                Dictionary<string, string> dicoParam = new Dictionary<string, string>();

                dicoParam.Add("IdUser", App.LoggedUser.Id.ToString());
                dicoParam.Add("IdEnchere", LaEnchere.Id.ToString());
                dicoParam.Add("PrixEnchere", param.ToString());
                if (await Tools.PostAsyncMultyParam("api/postEncherir", dicoParam))
                {
                    GetActualPrice();
                }
                else
                    Tools.ShowLongToast("erreur lors de l'envoi de l'enchère");
            }
            else
            {
                if (param <= 0)
                    Tools.ShowShortToast("L'enchère doit être supérieure à 0 ");
                else
                    Tools.ShowShortToast("L'enchère doit être infèrieure à la dernière enchère");
            }

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
            PrixMaximum = LEncherir.PrixEnchere;
        }


        /// <summary>
        /// s'execute en arrière plan pour rafraichir le prix de l'enchère
        /// </summary>
        /// <param name="loopBack"> vrai => le chrono redémarre à chaque fois qu'il est terminé || faux => le chrono s'arrête à la fin du premier passage</param>
        /// <param name="delay">le délai entre chaque déclenchement en secondes</param>
        private void GetActualPriceLoop(int delay)
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
                    PrixMaximum = LEncherir.PrixEnchere;
                });
                return LoopBack;
            });
        }
        #endregion

    }
}
