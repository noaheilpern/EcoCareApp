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
        public User CurrentUser { get; set; }
        public Seller CurrentSeller { get; set; }
        public RegularUser CurrentRegularUser { get; set; }

        public List<Country> CountriesList
        {
            get;
            set; }
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTc4MTI2QDMxMzkyZTM0MmUzMElEYjUvdHlTLzVYNzlUekZiOXRpYjBvd3F1MHc4dnEwbjdkdWMrTkwrblk9");
            InitializeComponent();
            NavigationPage p = new NavigationPage(new Views.Loading());
            MainPage = p;

        }
        
        protected async override void OnStart()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
           
            CountriesList = await proxy.GetCountriesAsync();
            /**
             CurrentUser = await proxy.LoginAsync("g@g.com", "123456");
             CurrentSeller = await proxy.GetSellerDataAsync(CurrentUser.UserName);
             Home h = new Home();
             h.Title = "Home";
             await App.Current.MainPage.Navigation.PushAsync(h);
              **/
           Page p = new Views.StartPage();
         
          p.Title = "Start Page";
          
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
