using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using EcoCareApp.ViewModels;

namespace EcoCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodePopUp : PopupPage
    {
        public BarcodePopUp()
        {
            this.BindingContext = new ProductPageViewModel();
            InitializeComponent();

        }
    }
}