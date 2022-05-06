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
    [QueryProperty(nameof(Param), nameof(Param))]
    public partial class MapView : ContentPage
    {
        MapViewModel viewModel;
        private string _param = "";
        public string Param { get => _param; set { _param = Uri.UnescapeDataString(value ?? string.Empty); OnPropertyChanged(); } }

        public MapView()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(_param, out var result);
            BindingContext = viewModel = new MapViewModel(result);

            Position p = await Tools.GetPosition();
            if (p != null)
            {
                mapTest.MoveToRegion(new MapSpan(p, 0.5, 0.5));

            }
            
        }

        private void Pin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            Tools.ShowLongToast("ok");
        }

        
    }
}