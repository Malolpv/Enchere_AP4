using Enchere_AP4.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Enchere_AP4.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}