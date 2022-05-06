using Enchere_AP4.Models;
using Enchere_AP4.Services;
using Enchere_AP4.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Enchere_AP4
{
    public partial class App : Application
    {

        private static User _loggedUser;
        public static User LoggedUser { get => _loggedUser; set => _loggedUser = value; }
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            LoggedUser = new User() { Id = 1,Nom = "Le Pavec", Prenom = "Malo"};

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async void LoadUser()
        {

            await Tools.PostAsyncMultyParam<User>("api/getUserByMailAndPass", new Dictionary<string, string>());

            
        }
        
    }
}
