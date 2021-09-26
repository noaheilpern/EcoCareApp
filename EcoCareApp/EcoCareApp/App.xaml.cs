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
            Page p = new Views.LogIn();
            p.Title = "Log in";
            MainPage = new NavigationPage(p) { BarBackgroundColor = Color.FromHex("#81cfe0") };
            //MainPage = new LogIn();
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
