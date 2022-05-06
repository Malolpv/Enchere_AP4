using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Enchere_AP4.Models
{
    public class User
    {
        #region attribut
        private int _id,_codePostal;
        private string _login, _password, _nom, _prenom, _adresse, _ville, _tel;
        private ObservableCollection<Enchere> _collEnchere;
        private Position userPosition;
        #endregion
        #region get/set

        public int Id { get => _id; set => _id = value; }

        public Position UserPosition { get => userPosition; set => userPosition = value; }
        public string Adresse { get => _adresse; set => _adresse = value; }
        public int CodePostal { get => _codePostal; set => _codePostal = value; }
        public string Login { get => _login; set => _login = value; }
        public string Password { get => _password; set => _password = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string Prenom { get => _prenom; set => _prenom = value; }
        public string Ville { get => _ville; set => _ville = value; }
        public string Tel { get => _tel; set => _tel = value; }
        public ObservableCollection<Enchere> CollEnchere { get => _collEnchere; set => _collEnchere = value; }

        #endregion

        #region constructor
        // to review 
        public User()
        {

            CollEnchere = new ObservableCollection<Enchere>(); 
        }

        public User(string login,string password , string nom, string prenom, string adresse,string ville,int codePostal, string tel)
        {
            Login = login;
            Password = password;
            Nom = nom;
            Prenom = prenom;
            _adresse = adresse;
            Ville = ville;
            Tel = tel;
            CodePostal = codePostal;

            CollEnchere = new ObservableCollection<Enchere>();
        }

        

        #endregion

        #region methods

        public async void GetLocation()
        {
            
            try
            {
                Location location = new Location();
                location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    userPosition = new Position(location.Latitude,location.Longitude);
                    //DependencyService.Get<IMessage>().LongAlert($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                DependencyService.Get<IMessage>().LongAlert("Handle not supported on device exception");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                DependencyService.Get<IMessage>().LongAlert("Handle not enabled on device exception");
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                DependencyService.Get<IMessage>().LongAlert("Handle permission exception");
            }
            catch (Exception ex)
            {
                // Unable to get location
                DependencyService.Get<IMessage>().LongAlert("Unable to get location");
            }

        }
        #endregion
    }
}
