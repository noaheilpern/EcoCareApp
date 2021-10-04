using EcoCareApp.ViewModels;
using EcoCareApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcoCareApp
{
    public partial class App : Application
    {
        public static bool IsDevEnv
        {
            get
            {
                return true;
            }
        }
        public App()
        {
            InitializeComponent();
            Page p = new StartPage(); 
            MainPage = new NavigationPage(new StartPage());
            


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
