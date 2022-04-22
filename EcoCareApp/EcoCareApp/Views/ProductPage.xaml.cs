using EcoCareApp.ViewModels;
using Syncfusion.XForms.PopupLayout;
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
    public partial class ProductPage : ContentPage
    {
        public ProductPage(ProductPageViewModel vm)
        {
            InitializeComponent();
            mainLayout.IsVisible = true; 
            this.BindingContext = vm;
            PopUpItself.IsVisible = false; 
            barcodePopup.Show();
        }

        private void showBarode_Clicked(object sender, EventArgs e)
        {
            PopUpItself.IsVisible = true;
            ProductPageViewModel vm = (ProductPageViewModel)this.BindingContext;
            vm.GenerateBarcode(); 
            barcodePopup.Show(); 
        }

        //private void showBarcode_Clicked(object sender, EventArgs e)
        //{
        //    popupLayout.Show(); 
        //}

    }
}