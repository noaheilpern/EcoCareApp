using EcoCareApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;
using EcoCareApp.Services;
using System.Threading.Tasks;
using EcoCareApp.Views;
using System.Runtime.Serialization;
using System.Reflection;
using System.Runtime.Serialization.Json;
using Rg.Plugins.Popup.Services;

namespace EcoCareApp.ViewModels
{
    class UserProfileViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        #region Events

        /// <summary>
        /// The declaration of the property changed event.
        /// </summary>

        #endregion


        #region Fields

        private static UserProfileViewModel userProfileViewModel;

        private readonly CardsItem passwordItem;
        private readonly CardsItem fnameItem;
        private readonly CardsItem lnameItem;
        private readonly CardsItem emailItem;
        private readonly CardsItem phoneItem;

        /// <summary>
        /// To store the user profile data collection. This property is readonly.
        /// </summary>
        public ObservableCollection<CardsItem> CardItems { get; }



        #endregion


        #region Properties

        /// <summary>
        /// Gets or sets the value of health profile view model.
        /// </summary>
        public static UserProfileViewModel BindingContext =>
            userProfileViewModel = PopulateData<UserProfileViewModel>("profile.json");

        /// <summary>
        /// Gets or sets the health profile items collection.
        /// </summary>





        #endregion
        public App app { get; } = Application.Current as App;

        #region Methods

        /// <summary>
        /// Populates the data for view model from json file.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="fileName">Json file to fetch data.</param>
        /// <returns>Returns the view model object.</returns>
        private static T PopulateData<T>(string fileName)
        {
            var file = "EssentialUIKit.Data." + fileName;

            var assembly = typeof(App).GetTypeInfo().Assembly;

            T data;

            using (var stream = assembly.GetManifestResourceStream(file))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                data = (T)serializer.ReadObject(stream);
            }

            return data;
        }

        /// <summary>
        /// Invoked when an item is selected from the health profile page.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void NavigateToNextPage(object selectedItem)
        {
            // Do something
        }

        #endregion





        public UserProfileViewModel()
        {
            InitCountries();
            this.SearchTerm = string.Empty;
            CountryNotSelected = false;


            App a = (App)App.Current;
            User u = a.CurrentUser;
            Password = u.Pass;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Email = u.Email;
            SelectedCountry = u.Country;
            UserName = u.UserName;


            passwordItem = new CardsItem
            {
                Category = "Password",
                CategoryValue = Password,
                CategoryError = PasswordError,
                ValidateCmd = new Command(() => ValidatePassword())
            };
            fnameItem = new CardsItem
            {
                Category = "FirstName",
                CategoryValue = FirstName,
                CategoryError = FirstNameError,
                ValidateCmd = new Command(() => ValidateFname())
            };
            lnameItem = new CardsItem
            {
                Category = "LastName",
                CategoryValue = LastName,
                CategoryError = LastNameError,
                ValidateCmd = new Command(() => ValidateLname())
            };
            emailItem = new CardsItem
            {
                Category = "Email",
                CategoryValue = Email,
                CategoryError = EmailError,
                ValidateCmd = new Command(() => ValidateEmail())
            };
            CardItems = new ObservableCollection<CardsItem>()
            {
                passwordItem,
                fnameItem,
                lnameItem,
                emailItem
            };

            if (a.CurrentRegularUser != null)
            {
                Birthday = a.CurrentRegularUser.Birthday;
                PeopleAtTheSameHouseHold = a.CurrentRegularUser.PeopleAtTheHousehold;
                app.Stars = (int)a.CurrentRegularUser.Stars;
            }
            else
            {
                PhoneNum = a.CurrentSeller.PhoneNum;
                phoneItem = new CardsItem
                {
                    Category = "PhoneNum",
                    CategoryValue = PhoneNum,
                    CategoryError = PhoneNumError,
                    ValidateCmd = new Command(() => ValidatePhoneNum())
                };
                CardItems.Add(phoneItem);
            }
        }

        public bool IsRegular
        {
            get
            {
                App a = (App)App.Current;
                return a.CurrentRegularUser != null;
            }
        }
        public bool IsSeller
        {
            get
            {
                App a = (App)App.Current;
                return a.CurrentRegularUser == null;
            }
        }
        public String UserName { get; }


        #region PeopleAtTheSameHouseHold 

        private bool showPeopleAtTheSameHouseHoldError;

        public bool ShowPeopleAtTheSameHouseHoldError
        {
            get => showPeopleAtTheSameHouseHoldError;
            set
            {
                showPeopleAtTheSameHouseHoldError = value;
                OnPropertyChanged("ShowPeopleAtTheSameHouseHoldError");
            }
        }
        private bool peopleAtTheSameHouseHoldTyped;
        public bool PeopleAtTheSameHouseHoldTyped
        {
            get => peopleAtTheSameHouseHoldTyped;
            set
            {
                peopleAtTheSameHouseHoldTyped = value;
                OnPropertyChanged("PeopleAtTheSameHouseHoldTyped");
            }
        }

        private int? peopleAtTheSameHouseHold;
        public int? PeopleAtTheSameHouseHold
        {
            get => peopleAtTheSameHouseHold;
            set
            {
                peopleAtTheSameHouseHold = value;
                if (string.IsNullOrEmpty(PeopleAtTheSameHouseHold.ToString()))
                    this.PeopleAtTheSameHouseHoldTyped = false;
                else
                    this.PeopleAtTheSameHouseHoldTyped = true;
                ValidatePeopleAtTheSameHouseHold();
                OnPropertyChanged("PeopleAtTheSameHouseHold");

            }
        }
        private string peopleAtTheSameHouseHoldError;
        public string PeopleAtTheSameHouseHoldError
        {
            get => peopleAtTheSameHouseHoldError;
            set
            {
                peopleAtTheSameHouseHoldError = value;
                OnPropertyChanged("PeopleAtTheSameHouseHoldError");

            }
        }
        private bool ValidatePeopleAtTheSameHouseHold()
        {
            this.ShowPeopleAtTheSameHouseHoldError = string.IsNullOrEmpty(PeopleAtTheSameHouseHold.ToString());
            if (!this.ShowPeopleAtTheSameHouseHoldError)
            {
                this.PeopleAtTheSameHouseHoldTyped = true;

                if (PeopleAtTheSameHouseHold < 1)
                {
                    this.ShowPeopleAtTheSameHouseHoldError = true;
                    this.PeopleAtTheSameHouseHoldError = ERROR_MESSAGES.BAD_PEOPLEATTHESAMEHOUSEHOLD;
                    return false;
                }
                else
                {
                    this.ShowPeopleAtTheSameHouseHoldError = false;
                    return true;
                }
            }
            else
            {
                this.PeopleAtTheSameHouseHoldError = ERROR_MESSAGES.REQUIRED_FIELD;
                PeopleAtTheSameHouseHoldTyped = false;
                return false;
            }

        }


        #endregion


        #region Password 
        private const int MIN_PASS_CHARS = 6;

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
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
                passwordItem.CategoryError = value;
            }
        }


        private bool ValidatePassword()
        {
            string password = passwordItem.CategoryValue;
            bool missing = string.IsNullOrEmpty(password);

            if (!missing)
            {
                if (password.Length < MIN_PASS_CHARS)
                {
                    PasswordError = ERROR_MESSAGES.BAD_PASSWORD;
                    return false;
                }
                else
                {
                    // no error
                    PasswordError = null;
                    return true;
                }
            }
            else
            {
                PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
                return false;
            }

        }


        #endregion

        #region country 


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
        }

        public void Changed(string category, string value)
        {
            if (category.Equals("Password"))
                Password = value;
            else if (category.Equals("FristName"))
                FirstName = value;
            else if (category.Equals("LastName"))
                LastName = value;
            else if (category.Equals("Email"))
                Email = value;
            else if (category.Equals("PhoneNum"))
                PhoneNum = value;

        }
        public ICommand TextChangedCommand => new Command(TextChanged);
        public void TextChanged()
        {

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

            this.FilteredCountries = new ObservableCollection<Country>(this.FilteredCountries.OrderBy(c => c.CountryName));
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
        public async void OnRefresh()
        {
            InitCountries();
            App a = (App)App.Current;
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            if (a.CurrentRegularUser != null)
            {
                a.CurrentRegularUser = await proxy.GetRegularUserDataAsync(a.CurrentUser.UserName);
                app.Stars = (int)a.CurrentRegularUser.Stars;

            }


        }
        #endregion




        public ICommand SelctionChanged => new Command(CountryChanged);

        public void CountryChanged(Object obj)
        {
            if (obj is Country)
            {
                SelectedCountry = ((Country)obj).CountryName;

            }
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
        public ICommand PopUpClosed => new Command(ClosePopUp);

        public void ClosePopUp()
        {
            PopupNavigation.Instance.PopAsync();
        }

        #endregion

        #region FirstName

        private string firstNameError;

        public string FirstNameError
        {

            get => firstNameError;

            set
            {
                firstNameError = value;
                OnPropertyChanged("FirstNameError");
                fnameItem.CategoryError = value;
            }

        }


        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private bool ValidateFname()
        {
            string firstName = fnameItem.CategoryValue;
            bool missing = string.IsNullOrEmpty(firstName);

            if (missing)
            {
                FirstNameError = ERROR_MESSAGES.REQUIRED_FIELD;
                return false;
            }
            else
            {
                // no error
                FirstNameError = null;
                return true;
            }
        }


        #endregion

        #region LastName

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
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
                lnameItem.CategoryError = value;
            }
        }

        private bool ValidateLname()
        {
            string lastName = lnameItem.CategoryValue;
            bool missing = string.IsNullOrEmpty(lastName);

            if (missing)
            {
                LastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
                return false;
            }
            else
            {
                // no error
                LastNameError = null;
                return true;
            }
        }

        #endregion

        #region Email

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
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
                emailItem.CategoryError = value;
            }
        }

        private bool ValidateEmail()
        {
            string email = emailItem.CategoryValue;
            bool missing = string.IsNullOrEmpty(email);
            if (!missing)
            {
                if (!Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    EmailError = ERROR_MESSAGES.BAD_EMAIL;
                    return false;
                }
                else
                {
                    // no error
                    EmailError = null;
                    return true;
                }
            }
            else
            {
                EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
                return false;
            }
        }
        #endregion




        //if user is business owner

        #region PhoneNum

        private string phoneNum;

        public string PhoneNum
        {

            get => phoneNum;
            set
            {

                phoneNum = value;
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
                phoneItem.CategoryError = value;
            }
        }

        private bool ValidatePhoneNum()
        {
            if (!IsRegular)
            {
                string phoneNum = phoneItem?.CategoryValue;
                bool missing = string.IsNullOrEmpty(phoneNum);

                if (!missing)
                {
                    if (!Regex.IsMatch(phoneNum, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
                    {
                        PhoneNumError = ERROR_MESSAGES.BAD_PHONE;
                        return false;
                    }
                    else
                    {
                        // no error
                        PhoneNumError = null;
                        return true;
                    }
                }
                else
                {
                    PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
                    return false;
                }

            }

            return false;
        }
        #endregion

        //else 
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

        #region Country
        public ICommand ChangeCountry => new Command(Countries);
        public void Countries()
        {
            CountriesPopUp cpu = new CountriesPopUp
            {
                BindingContext = this,
            };
            PopupNavigation.Instance.PushAsync(cpu);

        }

        #endregion

        public ICommand LogOut => new Command(LogOutUser);
        private async void LogOutUser()
        {
            App a = (App)App.Current;

            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            await proxy.LogOut(a.CurrentUser);
            a.CurrentRegularUser = null;
            a.CurrentSeller = null;
            a.CurrentUser = null;
            StartPage sp = new StartPage();
            NavigationPage.SetHasBackButton(sp, false);
            App.Current.MainPage = new NavigationPage(sp) { BarBackgroundColor = Color.FromHex("#81cfe0") };
        }
        public ICommand Update => new Command(UpdateUserAsync);


        private bool Validate()
        {
            bool b = ValidateEmail() & ValidatePassword() & ValidateFname() & ValidateLname();

            if (IsRegular)
                return b && ValidateBirthday() && ValidatePeopleAtTheSameHouseHold();
            else
                return b & ValidatePhoneNum();
        }

        private async void UpdateUserAsync()
        {
            bool worked = true;
            App a = (App)App.Current;
            bool success = true;
            if (a.CurrentUser.Email != email)
            {
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                success = await proxy.IsEmailExistAsync(email);
            }
            if (Validate() && success)
            {
                User appUser = a.CurrentUser;
                User u = new User
                {
                    Email = emailItem.CategoryValue,
                    FirstName = fnameItem.CategoryValue,
                    LastName = lnameItem.CategoryValue,
                    Pass = passwordItem.CategoryValue,
                    IsAdmin = false,
                    UserName = appUser.UserName,
                    Country = SelectedCountry,
                };

                App app = (App)App.Current;
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();


                if (app.CurrentRegularUser != null)
                {
                    RegularUser appRegularUser = app.CurrentRegularUser;
                    RegularUser ru = new RegularUser
                    {
                        UserNameNavigation = u,
                        Birthday = this.Birthday,
                        PeopleAtTheHousehold = (int)this.PeopleAtTheSameHouseHold,
                        InitialMeatsMeals = appRegularUser.InitialMeatsMeals,
                        DistanceToWork = appRegularUser.DistanceToWork,
                        LastElectricityBill = appRegularUser.LastElectricityBill,
                        Transportation = appRegularUser.Transportation,
                        UserName = appRegularUser.UserName,
                        VeganRareMeat = appRegularUser.VeganRareMeat,
                        Vegetarian = appRegularUser.Vegetarian,
                        UserCarbonFootPrint = appRegularUser.UserCarbonFootPrint,
                        Stars = appRegularUser.Stars,
                    };
                    if (!u.Equals(app.CurrentUser) || !ru.Equals(app.CurrentRegularUser))
                    {
                        isRefreshing = true;
                        worked = await proxy.UpdateUserAsync(ru);
                        isRefreshing = false;

                    }
                }
                else
                {

                    Seller s = new Seller
                    {
                        UserNameNavigation = u,
                        PhoneNum = phoneItem.CategoryValue,
                        UserName = u.UserName,


                    };
                    if (!u.Equals(app.CurrentUser) || !s.Equals(app.CurrentSeller))
                    {
                        isRefreshing = true;
                        worked = await proxy.UpdateSellerAsync(s);
                        isRefreshing = false;

                    }
                }

                //fp.Title = "Calculate your foot print";
                //await App.Current.MainPage.Navigation.PushAsync(fp);
                if (!worked)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Updating user data failed. Please check fields are filled as needed", "OK");

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Succeed", "detail(s) updated successfully", "great:)");

                }
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Updating user data failed. Please check fields are filled as needed", "OK");
            }


        }




    }
}
