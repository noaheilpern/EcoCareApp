﻿using EcoCareApp.Services;
using EcoCareApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcoCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeForSeller : ContentPage
    {
        public HomeForSeller()
        {
            this.BindingContext = new HomeViewModel();
            InitializeComponent();
        }


        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            //לבדוק את הפעולה הזו היא נראית לי שגויה
            Device.BeginInvokeOnMainThread(() =>
            {
                ScanResultText.Text = result.Text + "(Type:" + result.BarcodeFormat + ")";
                string str = result.Text;
                int id = 0;  
                while(str[0] >= '0' && str[0] <='9')
                {
                    id *= 10;
                    id = id + Convert.ToInt32(str[0]) - 48;
                    str = str.Substring(1);
                }
                string username = str;
                //now we will decrease stars to the user who bought the gift
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy(); 

            });
        }
    }
}