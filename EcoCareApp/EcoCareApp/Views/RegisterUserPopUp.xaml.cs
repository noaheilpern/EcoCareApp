using EcoCareApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace EcoCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUserPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public RegisterUserPopUp()
        {
            InitializeComponent();
        }
    }
}