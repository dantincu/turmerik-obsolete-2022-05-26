using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Turmerik.MusicalKeyboard.XmrnForms.App.Models;
using Turmerik.MusicalKeyboard.XmrnForms.App.ViewModels;
using Turmerik.MusicalKeyboard.XmrnForms.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Turmerik.MusicalKeyboard.XmrnForms.App.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}