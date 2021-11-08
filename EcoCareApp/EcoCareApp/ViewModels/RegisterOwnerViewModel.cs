using EcoCareApp.Models;
using EcoCareApp.Services;
using EcoCareApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
                OnPropertyChanged("ShowUserNameError");
            }
        }
        public bool userNameTyped;
        public bool UserNameTyped
        {
            get => userNameTyped;
            set
            {
                userNameTyped = value;
                OnPropertyChanged("UserNameTyped");
            }
        }

        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                if (string.IsNullOrEmpty(userName))
                {
                    this.UserNameTyped = false;
                }

                else
                {
                    UserNameTyped = true;
                    OnPropertyChanged("UserNameTyped");
                }
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

        private bool ValidateUserName()
        {
            this.ShowUserNameError = string.IsNullOrEmpty(UserName);
            if (!this.ShowUserNameError)
            {

                this.ShowUserNameError = false;
                return true;

            }
            else
            {
                this.UserNameError = ERROR_MESSAGES.REQUIRED_FIELD;
                return false;
            }
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

        private bool emailTyped;
        public bool EmailTyped
        {
            get => emailTyped;
            set
            {
                emailTyped = value;
                OnPropertyChanged("EmailTyped");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                if (string.IsNullOrEmpty(email))
                    EmailTyped = false;
                else
                    EmailTyped = true;
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

        private bool ValidateEmail()
        {

            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                    return false;
                }
                else
                {
                    this.ShowEmailError = false;
                    return true;
                }
            }
            else
            {
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
                return false;
            }
        }
        #endregion

        #region Birthday
        private bool birthdayTyped;
        public bool BirthdayTyped
        {
            get => birthdayTyped;
            set
            {
                birthdayTyped = value;
                OnPropertyChanged("BirthdayTyped");
            }
        }

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
        private DateTime birthday = DateTime.Today;
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
        private bool ValidateBirthday()
        {

            if (Birthday.CompareTo(DateTime.Today) >= 0)
            {
                this.ShowBirthdayError = true;
                this.BirthdayError = ERROR_MESSAGES.BAD_DATE;
                return false;
            }
            else
            {
                this.ShowBirthdayError = false;
            }
            return true;

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
        private bool passwordTyped;
        public bool PasswordTyped
        {
            get => passwordTyped;
            set
            {
                passwordTyped = value;
                OnPropertyChanged("PasswordTyped");
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                if (string.IsNullOrEmpty(Password))
                    this.PasswordTyped = false;
                else
                    this.PasswordTyped = true;
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
        private bool ValidatePassword()
        {
            this.ShowPasswordError = string.IsNullOrEmpty(Password);
            if (!this.ShowPasswordError)
            {
                this.PasswordTyped = true;

                if (Password.Length < MIN_PASS_CHARS)
                {
                    this.ShowPasswordError = true;
                    this.PasswordError = ERROR_MESSAGES.BAD_PASSWORD;
                    return false;
                }
                else
                {
                    this.ShowPasswordError = false;
                    return true;
                }
            }
            else
            {
                this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
                PasswordTyped = false;
                return false;
            }

        }


        #endregion

        #region FirstName
        private bool showFirstNameError;

        public bool ShowFirstNameError
        {
            get => showFirstNameError;
            set
            {
                showFirstNameError = value;
                OnPropertyChanged("ShowFirstNameError");
            }
        }

        public bool firstNameTyped;
        public bool FirstNameTyped
        {
            get => firstNameTyped;
            set
            {
                firstNameTyped = value;
                OnPropertyChanged("FirstNameTyped");
            }
        }
        private string firstNameError;

        public string FirstNameError
        {

            get => firstNameError;
            set
            {
                firstNameError = value;
                OnPropertyChanged("FirstNameError");
            }

        }


        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;

                this.ShowFirstNameError = string.IsNullOrEmpty(FirstName);
                if (ShowFirstNameError)
                {
                    this.FirstNameTyped = false;
                    this.FirstNameError = ERROR_MESSAGES.REQUIRED_FIELD;
                    OnPropertyChanged("FirstName");
                }
                else
                {
                    this.FirstNameTyped = true;
                    this.ShowFirstNameError = false;
                }

            }
        }



        #endregion

        #region LastName
        private bool showLastNameError;

        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        public bool lastNameTyped;
        public bool LastNameTyped
        {
            get => lastNameTyped;
            set
            {
                lastNameTyped = value;
                OnPropertyChanged("LastNameTyped");
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;

                this.ShowLastNameError = string.IsNullOrEmpty(LastName);
                if (ShowLastNameError)
                {
                    this.LastNameTyped = false;
                    this.LastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
                    OnPropertyChanged("LastName");
                }
                else
                {
                    this.LastNameTyped = true;
                    this.ShowLastNameError = false;
                    OnPropertyChanged("LastName");

                }
            }
        }
        private string lastNameError;

        public string LastNameError
        {

            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }


        }


        #endregion

        #region country 


        private string country;
        public string Country
        {
            get
            {
                return this.country;
            }
            set
            {
                this.country = value;
                OnPropertyChanged("Country");
            }
        }
        public List<Country> CountriesList
        {
            get
            {
                App a = (App)App.Current;
                return a.CountriesList;
            }
        }

        #endregion


        #region PhoneNum
        private bool showPhoneNumError;

        public bool ShowPhoneNumError
        {
            get => showPhoneNumError;
            set
            {
                showPhoneNumError = value;
                OnPropertyChanged("ShowPhoneNumError");
            }
        }

        private string phoneNum;

        public string PhoneNum
        {
            get => phoneNum;
            set
            {
                phoneNum = value;
                ValidatePhoneNum();
                OnPropertyChanged("PhoneNum");
            }
        }

        private string phoneNumError;

        public string PhoneNumError
        {
            get => phoneNumError;
            set
            {
                phoneNumError = value;
                OnPropertyChanged("PhoneNumError");
            }
        }
        public bool phoneNumTyped;
        public bool PhoneNumTyped
        {
            get => phoneNumTyped;
            set
            {
                phoneNumTyped = value;
                OnPropertyChanged("PhoneNumTyped");
            }
        }

        private void ValidatePhoneNum()
        {
            this.ShowPhoneNumError = string.IsNullOrEmpty(PhoneNum);
            if (!this.ShowPhoneNumError)
            {
                if (!Regex.IsMatch(this.PhoneNum, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
                {
                    this.ShowPhoneNumError = true;
                    this.PhoneNumError = ERROR_MESSAGES.BAD_PHONE;
                }
            }
            else
                this.PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion
        public ICommand ResigterUser => new Command(RegiUserAsync);
        public bool Valid { get; set; }
        private async void ValidateEmailAndUserNameAsync()
        {
            bool c = true;
            try
            {
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                Task<bool> t = proxy.IsEmailExistAsync(Email);
                bool b = await t;
                if (b)
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.EMAIL_EXIST;
                }

            }
            catch (Exception e)
            {
                this.ShowEmailError = true;
                this.EmailError = ERROR_MESSAGES.GENERAL_ERROR;
                c = false;
            }
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
                c = false;
            }
            Valid = c;
        }
        private bool Validate()
        {

            return ValidateEmail() && ValidatePassword() && ValidateUserName() && Valid;
        }

        private async void RegiUserAsync()
        {
            ValidateEmailAndUserNameAsync();
            if (Validate())
            {
                User u = new User
                {
                    Email = this.Email,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Pass = this.Password,
                    UserName = this.UserName,
                    IsAdmin = false,

                };
                Seller s = new Seller
                {
                    UserNameNavigation = u,
                    Country = this.Country,
                    PhoneNum = this.phoneNum,

                };
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                App a = (App)App.Current;
                //Loading l = new Loading();
                //l.Title = "Loading";
                //await App.Current.MainPage.Navigation.PushAsync(l);
                try
                {
                    Task<bool> t = proxy.RegisterBusinessOwner(s);
                    t.Wait();
                    Home h = new Home();
                    h.Title = "Home";
                    await App.Current.MainPage.Navigation.PushAsync(h);

                }
                catch(Exception e)
                {
                    //error message: register failed

                }
            }
            else
            {
                //error message: register failed
            }


        }





    }
}

