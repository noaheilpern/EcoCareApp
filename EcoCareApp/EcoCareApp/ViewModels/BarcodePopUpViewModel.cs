using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class BarcodePopUpViewModel
    {
        public string BarcodeValue { get; set; }
        public ICommand PopUpClosed => new Command(ClosePopUp);

        public void ClosePopUp()
        {
            PopupNavigation.Instance.PopAsync();
            //refresh star number
        }
    }
}
