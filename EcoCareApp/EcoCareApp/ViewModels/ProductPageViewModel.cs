using EcoCareApp.Models;
using EcoCareApp.Views;
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


        public virtual List<Sale> Sales { get; set; }
       
        public ICommand OpenPopUpPage => new Command(ToBarcode);

        public void ToBarcode()
        {
            App app = (App)App.Current;
            app.MainPage.Navigation.PushModalAsync(new BarcodePopUp());
            

        }
    }
}
