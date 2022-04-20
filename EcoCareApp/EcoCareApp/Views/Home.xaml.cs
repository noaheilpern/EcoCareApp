using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EcoCareApp.ViewModels;

namespace EcoCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            this.BindingContext = new HomeViewModel();
            InitializeComponent();
        }

        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
                {
                    ScanResultText.Text = result.Text + "(Type:" + result.BarcodeFormat + ")"; 
                });
        }
    }
}