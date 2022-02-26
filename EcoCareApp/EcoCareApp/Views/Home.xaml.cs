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

        private void Car_ItemTapped(object sender, Syncfusion.SfRadialMenu.XForms.ItemTappedEventArgs e)
        {
            ((HomeViewModel)this.BindingContext).CarTapped = true;
        }

        private void Elec_ItemTapped(object sender, Syncfusion.SfRadialMenu.XForms.ItemTappedEventArgs e)
        {
            ((HomeViewModel)this.BindingContext).ElecTapped = true;

        }

        private void Meat_ItemTapped(object sender, Syncfusion.SfRadialMenu.XForms.ItemTappedEventArgs e)
        {
            ((HomeViewModel)this.BindingContext).MeatTapped = true;

        }
    }
}