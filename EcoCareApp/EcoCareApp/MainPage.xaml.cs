using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using EcoCareApp.Services;

namespace EcoCareApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btn_Clicked(object sender, EventArgs e)
        {
            EcoCareAPIProxy api = EcoCareAPIProxy.CreateProxy();
            lbl.Text = await api.TestAsync();
        }
    }
}
