using EcoCareApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EcoCareApp.ViewModels
{
    class RegisterOwnerViewModel:INotifyPropertyChanged
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
                ValidateUserNameAsync();
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

        private async void ValidateUserNameAsync()
        {
            this.ShowUserNameError = string.IsNullOrEmpty(UserName);
            if (!this.ShowUserNameError)
            {
                try
                {
                    EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                    Task<bool> t = proxy.IsUserNameExistAsync(UserName);
                    bool b = await t;
                    if (b)
                    {
                        this.ShowUserNameError = true;
                        this.UserNameError = ERROR_MESSAGES.BAD_USERNAME;
                    }

                }
                catch (Exception e)
                {
                    this.ShowUserNameError = true;
                    this.UserNameError = ERROR_MESSAGES.GENERAL_ERROR;
                }
            }
            else
                this.UserNameError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

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
        #region Password 
        public const int MIN_PASS_CHARS = 6;

        private bool showPasswordError;

        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");

            }
        }
        private string passwordError;
        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");

            }
        }
        private void ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
            if (!this.ShowPasswordError)
            {
                if (Password.Length < MIN_PASS_CHARS)
                {
                    this.ShowPasswordError = true;
                    this.PasswordError = ERROR_MESSAGES.BAD_PASSWORD;
                }
            }
            else
                this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

    }
}
