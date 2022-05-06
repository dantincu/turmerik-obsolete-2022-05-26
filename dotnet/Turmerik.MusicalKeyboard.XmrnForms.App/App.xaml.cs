using System;
using Turmerik.MusicalKeyboard.XmrnForms.App.Services;
using Turmerik.MusicalKeyboard.XmrnForms.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Turmerik.MusicalKeyboard.XmrnForms.App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
