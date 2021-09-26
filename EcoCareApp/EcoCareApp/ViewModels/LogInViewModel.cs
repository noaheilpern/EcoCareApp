using EcoCareApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class LogInViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        //example
        //public async Task<string> GetTheString()
        //{
        //    CarpoolAPIProxy proxy = CarpoolAPIProxy.CreateProxy();
        //    this.Email = await proxy.GetStringAsync();
        //    return this.Email;
        //}

        public ICommand GetString => new Command(GetS);
        public async void GetS()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            this.Email = await proxy.GetStringAsync();
        }
    }
}
   

