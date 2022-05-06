using System;
using System.Collections.Generic;
using System.ComponentModel;
using Turmerik.MusicalKeyboard.XmrnForms.App.Models;
using Turmerik.MusicalKeyboard.XmrnForms.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Turmerik.MusicalKeyboard.XmrnForms.App.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}