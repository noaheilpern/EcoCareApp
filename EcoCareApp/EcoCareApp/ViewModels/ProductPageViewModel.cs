using EcoCareApp.Models;
using EcoCareApp.Views;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    public class ProductPageViewModel:INotifyPropertyChanged
    {


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public bool Active { get; set; }
        public string SellersUsername { get; set; }
        public int ProductId { get; set; }
        public string BarcodeValue { get; set; }

        public virtual List<Sale> Sales { get; set; }
        
        public ICommand ToBarcodePopUp => new Command(PopUp);
       
        public void PopUp()
        {
            PopupNavigation.Instance.PushAsync(new BarcodePopUp());
        }
        public void GenerateBarcode()
        {
            App app = (App)App.Current;
            string str = ProductId + app.CurrentRegularUser.UserName;
            BarcodeValue = str;
        }
    }
}
