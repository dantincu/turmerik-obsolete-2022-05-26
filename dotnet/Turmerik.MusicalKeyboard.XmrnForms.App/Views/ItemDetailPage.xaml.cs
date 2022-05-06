using System.ComponentModel;
using Turmerik.MusicalKeyboard.XmrnForms.App.ViewModels;
using Xamarin.Forms;

namespace Turmerik.MusicalKeyboard.XmrnForms.App.Views
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