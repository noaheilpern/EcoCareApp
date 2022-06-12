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
        private int stars; 
        public int Stars { get=> stars; 
            
            set
            {
                stars = value; 
                OnPropertyChanged("Stars");
            }
        }

        public List<Country> CountriesList
        {
            get;
            set; }
        private string serverStatus;
       
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTc4MTI2QDMxMzkyZTM0MmUzMElEYjUvdHlTLzVYNzlUekZiOXRpYjBvd3F1MHc4dnEwbjdkdWMrTkwrblk9");
            InitializeComponent();
            ContentPage p = new Views.Loading();
            MainPage = p;

        }
        
        protected async override void OnStart()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
           
            CountriesList = await proxy.GetCountriesAsync();
            if (CountriesList == null)
            {
                Views.Loading thePage = (Views.Loading)this.MainPage; 
                LoadingViewModel vm = (LoadingViewModel)thePage.BindingContext;
                vm.ServerStatus = "The server is down, try again later";
            }
            else
            {
                CurrentUser = await proxy.LoginAsync("g@g.com", "123456");
                //CurrentRegularUser = await proxy.GetRegularUserDataAsync(CurrentUser.UserName);
                CurrentSeller = await proxy.GetSellerDataAsync(CurrentUser.UserName);
                Home h = new Home();
                App.Current.MainPage = new NavigationPage(h);
                /**
             Page p = new Views.StartPage();
           
            p.Title = "Start Page";
            
            MainPage = new NavigationPage(p) { BarBackgroundColor = Color.FromHex("#81cfe0") };
                **/
            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
