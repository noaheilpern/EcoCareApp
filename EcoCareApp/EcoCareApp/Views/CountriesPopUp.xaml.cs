using EcoCareApp.ViewModels;
using Rg.Plugins.Popup.Pages;
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
    public partial class CountriesPopUp : PopupPage
    {
        public CountriesPopUp()
        {
            if(this.BindingContext == null)
            {
                BindingContext = new RegisterUserViewModel();
            }
            InitializeComponent();
        }
    }
}