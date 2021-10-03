using EcoCareApp.Models;
using EcoCareApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
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

        private string l;
        public string Label
        {
            get
            {
                return this.l;
            }
            set
            {
                this.l = value;
                OnPropertyChanged("Label");
            }
        }

        private string pass;
        public string Password
        {
            get
            {
                return this.pass;
            }
            set
            {
                this.pass = value;
                OnPropertyChanged("Password");
            }
        }
        public Page NextPage { get; set; }

        public ICommand LogIn => new Command(Log);
        public LogInViewModel()
        {

        }
        public LogInViewModel(string email, string password)
        {
            Email = email;
            Password = password;
            Log();
        }

        async void Log()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            User u = await proxy.LoginAsync(Email, Password);

            if (u != null)
            {
                
                
                Application.Current.Properties["IsLoggedIn"] = Boolean.TrueString;


                Page p = null;
              


                if (NavigateToPageEvent != null)
                    NavigateToPageEvent(p);
            }
            else
            {
                Label = "Email or password is incorrect. Please try again";
            }
        }
        public Action<Page> NavigateToPageEvent;
        //example
        /*
        public async Task<string> GetTheString()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            this.Email = await proxy.GetStringAsync();
            return this.Email;
        }
        */

        public ICommand GetString => new Command(GetS);
        public async void GetS()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            this.Email = await proxy.GetStringAsync();
        }
    }
}
   

