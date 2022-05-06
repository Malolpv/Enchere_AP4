using Enchere_AP4.Models;
using Enchere_AP4.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Enchere_AP4.Views
{
    public partial class Accueil : ContentPage
    {
        AccueilViewModel viewModel;
        public Accueil()
        {
            InitializeComponent();
            BindingContext = viewModel = new AccueilViewModel(App.LoggedUser);
            
        
        }
        

        
    }
}