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

        #region Property

        /// <summary>
        /// Gets or sets the property that has been displays the category.
        /// </summary>
        [DataMember(Name = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the property that has been displays the category value.
        /// </summary>
        [DataMember(Name = "categoryValue")]
        public string CategoryValue { get; set; }

        /// <summary>
        /// Gets or sets the property that has been displays the category error.
        /// </summary>
        [DataMember(Name = "categoryError")]
        public string CategoryErorr { get; set; }

        #endregion

        #region Fields

        private static UserProfileViewModel userProfileViewModel;

        /// <summary>
        /// To store the user profile data collection.
        /// </summary>

        private ObservableCollection<CardsItem> cardItems;
        public ObservableCollection<CardsItem> CardItems
        {
            get => cardItems;
            set
            {
                cardItems = value;
                OnPropertyChanged("CardItems");
            }

           
        }
       


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

            InitCountries();

            App a = (App)App.Current;
            User u = a.CurrentUser;
            Password = u.Pass;
            FirstName = u.FirstName;
            LastName = u.LastName;
            Email = u.Email;
            SelectedCountry = u.Country;
            UserName = u.UserName;
            cardItems = new ObservableCollection<CardsItem>();
            cardItems.Add(new CardsItem
            {
                Category = "Password",
                CategoryValue = Password,
                CategoryError = PasswordError,

            });
            
            cardItems.Add(new CardsItem
            {
                Category = "FirstName",
                CategoryValue = FirstName,
                CategoryError = firstNameError,

            });
            cardItems.Add(new CardsItem
            {
                Category = "LastName",
                CategoryValue = LastName,
                CategoryError = lastNameError,


            });
            cardItems.Add(new CardsItem
            {
                Category = "Email",
                CategoryValue = Email,
                CategoryError = emailError,
            });

            if (a.CurrentRegularUser != null)
            {
                Birthday = a.CurrentRegularUser.Birthday;
                PeopleAtTheSameHouseHold = a.CurrentRegularUser.PeopleAtTheHousehold;
                Stars = (int)a.CurrentRegularUser.Stars;
                
            }
            else
            {
                PhoneNum = a.CurrentSeller.PhoneNum;
                cardItems.Add(new CardsItem
                {
                    Category = "PhoneNum",
                    CategoryValue = PhoneNum,
                    CategoryError = phoneNumError,
                });
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
        public void OnRefresh()
        {
            InitCountries();
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



        public int stars;
        public int Stars {
            get => stars;
            set
            {
                stars = value;
                OnPropertyChanged("Stars");
            }

        }


        //if user is business owner

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
                if (string.IsNullOrEmpty(PhoneNum))
                    this.PhoneNumTyped = false;
                else if(!IsRegular)
                    this.PhoneNumTyped = true;
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

        private bool ValidatePhoneNum()
        {
            if (!IsRegular)
            {

                this.ShowPhoneNumError = string.IsNullOrEmpty(PhoneNum);
                if (!this.ShowPhoneNumError)
                {
                    if (!Regex.IsMatch(this.PhoneNum, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
                    {
                        this.ShowPhoneNumError = true;
                        this.PhoneNumError = ERROR_MESSAGES.BAD_PHONE;
                        return false;
                    }
                    return true;
                }
                else
                    this.PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
                return false;
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
        private void LogOutUser()
        {
            App a = (App)App.Current;
            a.CurrentRegularUser = null;
            a.CurrentSeller = null;
            a.CurrentUser = null;

            StartPage sp = new StartPage();
            NavigationPage.SetHasBackButton(sp,false);
            App.Current.MainPage = new NavigationPage(sp) { BarBackgroundColor = Color.FromHex("#81cfe0") };
        }
        public ICommand Update => new Command(UpdateUserAsync);


        private bool Validate()
        {
            bool b = ValidateEmail() && ValidatePassword();
            if (IsRegular)
                return b && ValidateBirthday() && ValidatePeopleAtTheSameHouseHold();
            else
                return b && ValidatePhoneNum();
        }

        private async void UpdateUserAsync()
        {
            bool worked = true;
            App a = (App)App.Current;
            bool success = true;
            if(a.CurrentUser.Email != email) {
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                success = await proxy.IsEmailExistAsync(email);
            }
            if (Validate() && success)
            {
                User appUser = a.CurrentUser;
                User u = new User
                {
                    Email = cardItems.Where(c => c.Category.Equals("Email")).FirstOrDefault().CategoryValue,
                    FirstName = cardItems.Where(c => c.Category.Equals("FirstName")).FirstOrDefault().CategoryValue,
                    LastName = cardItems.Where(c => c.Category.Equals("LastName")).FirstOrDefault().CategoryValue,
                    Pass = cardItems.Where(c => c.Category.Equals("Password")).FirstOrDefault().CategoryValue,
                    Country = this.SelectedCountry,
                    IsAdmin = false,
                    UserName = appUser.UserName,
                    
                };
                App app = (App)App.Current;
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                

                if (app.CurrentRegularUser!=null)
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
                        
                    };
                    if(!u.Equals(app.CurrentUser)|| !ru.Equals(app.CurrentRegularUser))
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
                        PhoneNum = this.PhoneNum,
                        UserName = u.UserName,
                        

                    };
                    if(!u.Equals(app.CurrentUser)|| !s.Equals(app.CurrentSeller))
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
