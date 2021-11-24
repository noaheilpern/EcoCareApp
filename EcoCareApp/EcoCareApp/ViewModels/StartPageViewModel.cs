using EcoCareApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class StartPageViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        public ICommand ToRegisterOwner => new Command(ToRegisterO);
        public async void ToRegisterO()
        {
            App a = (App)App.Current;
            RegisterOwner ro = new RegisterOwner();
            ro.Title = "Register for a business owner";
            await App.Current.MainPage.Navigation.PushAsync(ro);
        }
        
        public ICommand ToRegisterUser => new Command(ToRegisterU);
        async void ToRegisterU()
        {
            App a = (App)App.Current;
            RegisterUser ru = new RegisterUser();
            ru.Title = "Register for a user";
            await App.Current.MainPage.Navigation.PushAsync(ru);
        }

        
         public ICommand ToLogIn => new Command(TOLogIn);
        async void TOLogIn()
        {
            App a = (App)App.Current;
            LogIn li = new LogIn();
            await App.Current.MainPage.Navigation.PushAsync(li);
        }

    }
}
