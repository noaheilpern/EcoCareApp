﻿using System;
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
        public const string GENERAL_ERROR = "Something went bad. Please try again";
        public const string BAD_PASSWORD = "Password has to be 6 charechters minimum";
        public const string BAD_DATE = "You must be older than today years old to use this app!";
    }
    class RegisterUserViewModel : INotifyPropertyChanged
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
                ValidateEmailAsync();
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

        private async void ValidateEmailAsync()
        {
            if (!this.ShowUserNameError)
            {
                
            }
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
                else
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
            }
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region Birthday
        private bool showBirthdayError;

        public bool ShowBirthdayError
        {
            get => showBirthdayError;
            set
            {
                showBirthdayError = value;
                OnPropertyChanged("ShowBirthdayError");
            }
        }
        private DateTime birthday;
        public DateTime Birthday
        {
            get => birthday;
            set
            {
                birthday = value;
                ValidateBirthday();
                OnPropertyChanged("Birthday");

            }
        }
        private string birthdayError;
        public string BirthdayError
        {
            get => birthdayError;
            set
            {
                birthdayError = value;
                
                OnPropertyChanged("BirthdayError");

            }
        }
        private void ValidateBirthday()
        {
            
            if (Birthday.CompareTo(DateTime.Today)>=0)
            {
                    this.ShowBirthdayError = true;
                    this.BirthdayError = ERROR_MESSAGES.BAD_DATE;
            }
            
        }
        #endregion

        #region Password 
        private const int MIN_PASS_CHARS = 6;

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

        IComm



    }
}

