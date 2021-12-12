using EcoCareApp.ViewModels;
using EcoCareApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EcoCareApp.Models;
using System.Collections.Generic;
using EcoCareApp.Services;

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
        public List<Country> CountriesList
        {
            get;
            set; }
        public App()
        {
            InitializeComponent();
            Page p = new Views.Loading();
            MainPage = p;

        }
        
        protected async override void OnStart()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            base.oncreate(bandle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            CountriesList = await proxy.GetCountriesAsync();
            Page p = new Views.StartPage(); 
            //p.Title = "Start Page";
            MainPage = new NavigationPage(p) { BarBackgroundColor = Color.FromHex("#81cfe0") };
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
