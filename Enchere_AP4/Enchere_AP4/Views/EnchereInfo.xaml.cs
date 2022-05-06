using Enchere_AP4.Models;
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
    [QueryProperty(nameof(Param),nameof(Param))]
    public partial class EnchereInfo : ContentPage
    {
        EnchereInfoViewModel viewModel;

        private string _param = "";
        public string Param { get => _param; set { _param = Uri.UnescapeDataString(value ?? string.Empty); OnPropertyChanged(); } }

        public EnchereInfo()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(_param, out var result);
            BindingContext = viewModel = new EnchereInfoViewModel(result);
        }
    }

}