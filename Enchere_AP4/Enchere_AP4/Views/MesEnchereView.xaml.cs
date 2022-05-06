using Enchere_AP4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Enchere_AP4.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MesEnchereView : ContentPage
    {

        MesEncheresViewModel viewModel;
        public MesEnchereView()
        {
            InitializeComponent();
            BindingContext = viewModel = new MesEncheresViewModel();
        }
    }
}