using EcoCareApp.Models;
using EcoCareApp.Services;
using EcoCareApp.Views;
using Rg.Plugins.Popup.Pages;
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
        private int stars;
        public int Stars {
            get=> stars;
            set
            {
                stars = value;
                OnPropertyChanged("Stars");
            } 
        }

        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public bool Active { get; set; }
        public string SellersUsername { get; set; }
        public int ProductId { get; set; }
        public string BarcodeValue { get; set; }

        bool hasEnoughStars; 
        public bool HasEnoughStars
        {
            get => hasEnoughStars; 
            
            set
            {
                hasEnoughStars = value;
                OnPropertyChanged("HasEnoughStars");
            }
        }

        bool hasNotEnoughStars; 
        
        public bool HasNotEnoughStars
        {
            get => hasNotEnoughStars;
            
            set
            {
                hasNotEnoughStars = value;
                OnPropertyChanged("HasNotEnoughStars");
            }
        }

        public virtual List<Sale> Sales { get; set; }
        public App app { get; } = Application.Current as App;


        public ICommand ToBarcodePopUp => new Command(PopUp);
        
        public ProductPageViewModel()
        {
            App a = (App)App.Current;
            Stars = a.Stars; 
        }
        public void PopUp()
        {
            GenerateBarcode();
            PopupPage p = new BarcodePopUp();


            BarcodeValue = GenerateBarcode();

            p.BindingContext = this; 




            PopupNavigation.Instance.PushAsync(p);
            
        }
        public string GenerateBarcode()
        {
            App app = (App)App.Current;
            string str = ProductId + "/" + app.CurrentRegularUser.UserName;
            BarcodeValue = str;
            return str;
        }

        public ICommand PopUpClosed => new Command(ClosePopUp);

        public async void ClosePopUp()
        {
            await PopupNavigation.Instance.PopAsync();
            //refresh star number
            App a = (App)App.Current;
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();

            a.CurrentRegularUser = await proxy.GetRegularUserDataAsync(a.CurrentUser.UserName);
            a.Stars = (int)a.CurrentRegularUser.Stars;

        }


    }
}
