using EcoCareApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class CarbonFootPrintViewModel
    {
        private double carbonFootPrint; 
        public double CarbonFootPrint { get; set; }

        public CarbonFootPrintViewModel()
        {
            App a = (App)App.Current;
            CarbonFootPrint = (double)a.CurrentRegularUser.UserCarbonFootPrint;
        }
        public ICommand PopUpClosed => new Command(Close);
        public async void Close()
        {
            Home h = new Home();
            h.Title = "Home";
            await App.Current.MainPage.Navigation.PushAsync(h);
        }



    }
}
