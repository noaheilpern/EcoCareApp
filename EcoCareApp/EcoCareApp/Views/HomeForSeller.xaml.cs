using EcoCareApp.Services;
using EcoCareApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace EcoCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeForSeller : ContentView
    {
        public HomeForSeller()
        {
            HomeViewModel h = new HomeViewModel();
            this.BindingContext = h;
            h.BarcodeEvent += ZXingScannerView_OnScanResult;

            InitializeComponent();
            Scanner.AutoFocus();
            Scanner.IsScanning = true; 
        }
       

        private async void ZXingScannerView_OnScanResult(ZXing.Result result)
        {

            try
            {
                Scanner.IsScanning = false; 
                string str = result.Text;
                int id = 0;
                while (str[0] != '/')
                {
                    id *= 10;
                    id = id + Convert.ToInt32(str[0]) - 48;
                    str = str.Substring(1);
                }
                str = str.Substring(1);
                string username = str;
                //check if the current seller is the owner of the product
                App a = (App)App.Current;
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                bool success = true;
                bool accessable = true;
                string sellerOfProductUserName = await proxy.GetSellerUserName(id);
                if (a.CurrentSeller.UserName.Equals(sellerOfProductUserName))
                {
                    //now we will decrease stars to the user who bought the gift
                    success = proxy.DecreaseStarsAfterBuying(username, id).Result;
                }
                else
                {
                    accessable = false; 
                }

                
                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread( () =>
                    {
                        Navigation.PopModalAsync();
                        if (success && accessable)
                        {
                             App.Current.MainPage.DisplayAlert("Succeed", "Sale done succefully", "Amazing");

                        }
                        else if(!accessable)
                        {
                             App.Current.MainPage.DisplayAlert("Access denied", "You aren't the owner of this product.", "OK");

                        }
                        else
                        {
                            App.Current.MainPage.DisplayAlert("Error", "Something went worng:/ Please try again", "OK");

                        }
                    });
                Home h = new Home();
                await App.Current.MainPage.Navigation.PushAsync(h);
                Scanner.IsScanning = true;

            }


            catch (Exception ee)
            {
                Console.WriteLine(ee);
                return; 
            }
            
        }

    }

}