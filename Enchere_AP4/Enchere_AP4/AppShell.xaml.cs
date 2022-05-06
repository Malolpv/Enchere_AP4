using Enchere_AP4.ViewModels;
using Enchere_AP4.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Enchere_AP4
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(EnchereInfo), typeof(EnchereInfo));
            Routing.RegisterRoute(nameof(Encherir), typeof (Encherir));
            Routing.RegisterRoute(nameof(MapView), typeof (MapView));
            Routing.RegisterRoute(nameof(EncherirInverse), typeof (EncherirInverse));
            Routing.RegisterRoute(nameof(EncherirFlash), typeof (EncherirFlash));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
