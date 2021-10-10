using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EcoCareApp.Services;

namespace EcoCareApp.ViewModels
{
    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "Something's missing. Please check and try again.";
        public const string BAD_EMAIL = "Email isn't valid";
        public const string BAD_USERNAME = "This username is already exist. Please try another one:)"; 
    }
    class RegisterUserViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region UserName
        private bool showUserNameError; 

        public bool ShowUserNameError
        {
            get => showUserNameError; 
            set
            {
                showUserNameError = value; 
                OnPropertyChanged("ShowserNameError"); 
            }
        }
        private string userName;
        public string UserName
        {
            get => userName; 
            set
            {
                userName = value;
                ValidateUserName();
                OnPropertyChanged("UserName");
            }
        }
        private string userNameError; 
        
        public string UserNameError
        {

           get => userNameError;
           set
           {
                userNameError = value;
                OnPropertyChanged("UserNameError");
           }

        
        }
        private async void ValidateUserName()
        {
            this.ShowUserNameError = string.IsNullOrEmpty(UserName);
            if (!this.ShowUserNameError)
            {
                try
                {
                    EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                    Task<bool> t = proxy.IsUserNameExistAsync(UserName);
                    bool b = await t;
                    return b; 

                }
                catch(Exception e)
                {
                    this.ShowUserNameError = true;
                    this.UserNameError = ERROR_MESSAGES.BAD_USERNAME;
                }
            }
            else
                this.UserNameError = ERROR_MESSAGES.REQUIRED_FIELD;
        
    }

        #region Email
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion
    }
}
