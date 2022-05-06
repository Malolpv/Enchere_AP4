using Enchere_AP4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Enchere_AP4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MagasinMapView : ContentPage
    {
        MagasinMapViewModel viewModel;
        public MagasinMapView()
        {
            InitializeComponent();
            BindingContext = viewModel = new MagasinMapViewModel();

            
        }

        private void Pin_MarkerClicked(object sender, Xamarin.Forms.Maps.PinClickedEventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //initialisation de la position de la carte
            MapInit();
        }

        private async void MapInit()
        {
            mapTest.MoveToRegion(MapSpan.FromCenterAndRadius(await Tools.GetPosition(), Distance.FromKilometers(10)));
        }


        #region boutons zoom 
        /// <summary>
        /// les trois méthodes ci-dessous permettent d'ajuster le zoom de la map selon en se basant sur le centre de la region visible
        /// </summary>
        

        private void btn_5km_Clicked(object sender, EventArgs e)
        {
            mapTest.MoveToRegion(MapSpan.FromCenterAndRadius(mapTest.VisibleRegion.Center, Distance.FromKilometers(5)));
        }

        private void btn_10km_Clicked(object sender, EventArgs e)
        {
            mapTest.MoveToRegion(MapSpan.FromCenterAndRadius(mapTest.VisibleRegion.Center, Distance.FromKilometers(10)));
        }

        private void btn_50km_Clicked(object sender, EventArgs e)
        {
            mapTest.MoveToRegion(MapSpan.FromCenterAndRadius(mapTest.VisibleRegion.Center, Distance.FromKilometers(50)));
        }

        #endregion
    }
}