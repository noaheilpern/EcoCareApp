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
            //vm.OpenPopUpEvent += showBarcode_Clicked;
            this.BindingContext = vm; 
        }

        //private void showBarcode_Clicked(object sender, EventArgs e)
        //{
        //    popupLayout.Show(); 
        //}

    }
}