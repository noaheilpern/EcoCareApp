using EcoCareApp.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class CarbonFootPrintViewModel
    {
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
            NavigationPage.SetHasBackButton(h, false);
            h.Title = "Home";

            NavigationPage.SetHasBackButton(h, false);
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(h);
        }



    }
}
