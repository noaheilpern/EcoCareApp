using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EcoCareApp.Models;
using EcoCareApp.Services;
using EcoCareApp.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    public static partial class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "This is a required field. Please fill it.";
        public const string BAD_EMAIL = "Email isn't valid";
        public const string EMAIL_EXIST = "Email is already exist";
        public const string BAD_PHONE = "Phone number is not valid";
        public const string BAD_USERNAME = "This username is already exist. Please try another one:)";
        public const string GENERAL_ERROR = "Something went bad. Please try again";
        public const string BAD_PASSWORD = "Password has to be 6 charechters minimum";
        public const string BAD_DATE = "You must be older than today years old to use this app!";
    }
    public class ShowPasswordTriggerAction : TriggerAction<ImageButton>, INotifyPropertyChanged
    {
        public string ShowIcon { get; set; }
        public string HideIcon { get; set; }

        bool _hidePassword = true;

        public bool HidePassword
        {
            set
            {
                if (_hidePassword != value)
                {
                    _hidePassword = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HidePassword)));
                }
            }
            get => _hidePassword;
        }

        protected override void Invoke(ImageButton sender)
        {
            sender.Source = HidePassword ? ShowIcon : HideIcon;
            HidePassword = !HidePassword;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
            
            if (Birthday.CompareTo(DateTime.Today)>=0)
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
                if(ShowFirstNameError)
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
                if(ShowLastNameError)
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
        public RegisterUserViewModel()
        {
            this.SearchTerm = string.Empty;
            CountryNotSelected = true;

            InitCountries();
        }

        public ICommand CountrySelectedCommand => new Command(Selected);

        public async void Selected(Object obj)
        {
            if (obj is Country)
            {
                SelectedCountry = ((Country)obj).CountryName;
                await PopupNavigation.Instance.PopAsync();

            }


        }

        private void InitCountries()
        {
            isRefreshing = true;
            App theApp = (App)App.Current;
            this.allCountriesList = theApp.CountriesList;

            this.FilteredCountries = new ObservableCollection<Country>(this.AllCountriesList.OrderBy(c => c.CountryName));
            SearchTerm = string.Empty;
            IsRefreshing = false;

        }

        private string selectedCountry;
        public string SelectedCountry
        {
            get
            {
                return this.selectedCountry;
            }
            set
            {
                this.selectedCountry = value;

                if (string.IsNullOrEmpty(selectedCountry))
                {
                    this.CountrySelected = false;
                    OnPropertyChanged("CountrySelected");

                }

                else
                {
                    CountrySelected = true;
                    OnPropertyChanged("CountrySelected");
                }

                OnPropertyChanged("SelectedCountry");
            }
        }
        private List<Country> allCountriesList;
        public List<Country> AllCountriesList
        {
            get
            {
                return allCountriesList;
            }
            set
            {
                allCountriesList = value;
                OnPropertyChanged("AllCountriesList");
            }
        }
        private ObservableCollection<Country> filteredCountries;
        public ObservableCollection<Country> FilteredCountries
        {
            get
            {
                return this.filteredCountries;
            }
            set
            {
                if (this.filteredCountries != value)
                {
                    this.filteredCountries = value;
                    OnPropertyChanged("FilteredCountries");
                }
            }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return this.searchTerm;
            }
            set
            {
                if (this.searchTerm != value)
                {
                    this.searchTerm = value;
                    OnTextChanged(value);
                    OnPropertyChanged("SearchTerm");

                }
            }
        }

        #region Search
        public void OnTextChanged(string search)
        {
            //Filter the list of countries based on the search term
            if (this.AllCountriesList == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                foreach (Country c in this.AllCountriesList)
                {
                    if (!this.FilteredCountries.Contains(c))
                        this.FilteredCountries.Add(c);


                }
            }
            else
            {
                search = search.ToLower();
                foreach (Country c in this.AllCountriesList)
                {
                    string countryNameString = $"{c.CountryName.ToLower()}";



                    if (!this.FilteredCountries.Contains(c) &&
                       countryNameString.Contains(search))
                        this.FilteredCountries.Add(c);
                    else if (this.FilteredCountries.Contains(c) &&
                        !countryNameString.Contains(search))
                        this.FilteredCountries.Remove(c);
                }
            }

            FilteredCountries = new ObservableCollection<Country>(this.FilteredCountries.OrderBy(c => c.CountryName));
            
        }
        #endregion

        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        public ICommand RefreshCommand => new Command(OnRefresh);
        public void OnRefresh()
        {
            InitCountries();
        }
        #endregion


        #endregion



       
        public ICommand ResigterUser => new Command(RegiUserAsync);
        public bool Valid { get; set; }
        private async Task<bool> ValidateEmailAndUserNameAsync()
        {
            bool c = true;
            try
            {
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                bool t = await proxy.IsEmailExistAsync(Email);
                if (t)
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
                bool t = await proxy.IsUserNameExistAsync(UserName);
                if (t)
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
            return c;
        }
        private bool Validate()
        {
            
            return ValidateBirthday() && ValidateEmail() && ValidatePassword() && ValidateUserName() && Valid; 
        }

        private async void RegiUserAsync()
        {
            bool success = await ValidateEmailAndUserNameAsync();
            if(Validate() && success)
            {
                User u = new User
                {
                    Email = this.Email,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Pass = this.Password,
                    UserName = this.UserName,
                    Country = this.SelectedCountry,
                    IsAdmin = false,

                };
                RegularUser ru = new RegularUser
                {
                    UserName = this.UserName,
                    UserNameNavigation = u,
                    Birthday = this.Birthday,
                    Stars = 0,
                };
                App a = (App)App.Current;
                FootPrintCalc fp = new FootPrintCalc(ru);
                fp.Title = "Calculate your foot print";
                await App.Current.MainPage.Navigation.PushAsync(fp);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Registeration failed. Please check fields are filled as needed", "OK");
            }


        }

        public ICommand ToCountries => new Command(Countries);
        public void Countries()
        {
            CountriesPopUp cpu = new CountriesPopUp
            {
                BindingContext = this,
            };
            PopupNavigation.Instance.PushAsync(cpu);

        }
        public ICommand MoveToLogIn => new Command(ToLogin);

        public async void ToLogin()
        {
            LogIn l = new LogIn();
            
            l.Title = "Calculate your foot print";
            await App.Current.MainPage.Navigation.PushAsync(l) ;

        }


        public ICommand SelctionChanged => new Command(CountryChanged);

        public void CountryChanged(Object obj)
        {
            if (obj is Country)
            {
                SelectedCountry = ((Country)obj).CountryName;

            }
        }
        public ICommand PopUpClosed => new Command(ClosePopUp);

        public void ClosePopUp()
        {
            PopupNavigation.Instance.PopAsync();
        }

        private bool countryNotSelected; 

        public bool CountryNotSelected
        {
            get
            {
                return this.countryNotSelected;
            }
            set

            {
                countryNotSelected = value;


                OnPropertyChanged("CountryNotSelected");
            }
        }
        private bool countrySelected;

        public bool CountrySelected
        {
            get
            {
                return this.countrySelected;
            }
            set

            {
                countrySelected = value;
                CountryNotSelected = !value;

                OnPropertyChanged("CountrySelected");
            }
        }

    }
}

