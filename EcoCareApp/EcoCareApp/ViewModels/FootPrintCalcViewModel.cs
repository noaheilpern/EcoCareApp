using EcoCareApp.Models;
using EcoCareApp.Services;
using EcoCareApp.Views;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    public static partial class ERROR_MESSAGES
    {
        public const string BAD_MEATMEALS = "You can't eat less than zero meat meals per week";
        public const string BAD_ELECTRICITY_PAID = "You can't pay less than 0" +
            " dollars to the electricity company";
        public const string BAD_WORKDISTANCE = "Thw work distance cna't be less than 0";
        public const string BAD_PEOPLEATTHESAMEHOUSEHOLD = "You can't be less than 1 in the same household";
    }
        class FootPrintCalcViewModel: INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        
        private RegularUser regularUser;
        public RegularUser RegularUser
        {
            get
            {
                return this.regularUser;
            }
            set
            {
                this.regularUser = value;
                OnPropertyChanged("RegularUser");
            }
        }
        public FootPrintCalcViewModel(RegularUser ru)
        {
            regularUser = ru;
            

        }
        #region MeatMeals
        private const int MIN_MEAT_MEALS = 0;

        private bool showMeatMealsError;

        public bool ShowMeatMealsError
        {
            get => showMeatMealsError;
            set
            {
                showMeatMealsError = value;
                OnPropertyChanged("ShowMeatMealsError");
            }
        }
        private bool meatMealsTyped;
        public bool MeatMealsTyped
        {
            get => meatMealsTyped;
            set
            {
                meatMealsTyped = value;
                OnPropertyChanged("MeatMealsTyped");
            }
        }
        private int? meatMeals;
        public int? MeatMeals
        {
            get => meatMeals;
            
            set
            {
                meatMeals = value;
                if (string.IsNullOrEmpty(MeatMeals.ToString()))
                    this.MeatMealsTyped = false;
                else
                    this.MeatMealsTyped = true;
                ValidateMeatMeals();
                OnPropertyChanged("MeatMeals");

            }
        }
        private string meatMealsError;
        public string MeatMealsError
        {
            get => meatMealsError;
            set
            {
                meatMealsError = value;
                OnPropertyChanged("MeatMealsError");

            }
        }
        private bool ValidateMeatMeals()
        {
            this.ShowMeatMealsError = string.IsNullOrEmpty(MeatMeals.ToString());
            if (!this.ShowMeatMealsError)
            {
                this.MeatMealsTyped = true;

                if (meatMeals < MIN_MEAT_MEALS)
                {
                    this.ShowMeatMealsError = true;
                    this.MeatMealsError = ERROR_MESSAGES.BAD_MEATMEALS;
                    return false;
                }
                else
                {
                    this.ShowMeatMealsError = false;
                    return true;
                }
            }
            else
            {
                this.MeatMealsError = ERROR_MESSAGES.REQUIRED_FIELD;
                MeatMealsTyped = false;
                return false;
            }

        }


        #endregion


        #region Vegetarian
        private bool vegetarian;
        public bool Vegetarian
        {
            get
            {
                return this.vegetarian;
            }
            set
            {
                this.vegetarian = value;
                OnPropertyChanged("Vegetarian");
            }
        }
        #endregion

        #region Vegan
        private bool vegan; 
        public bool Vegan
        {
            get
            {
                return this.vegan;
            }
            set
            {
                this.vegan = value;
                OnPropertyChanged("Vegan");
            }
        }
        #endregion

        #region ElectricityAmount 

        private bool showElectricityAmountError;

        public bool ShowElectricityAmountError
        {
            get => showElectricityAmountError;
            set
            {
                showElectricityAmountError = value;
                OnPropertyChanged("ShowElectricityAmountError");
            }
        }
        private bool electricityAmountTyped;
        public bool ElectricityAmountTyped
        {
            get => electricityAmountTyped;
            set
            {
                electricityAmountTyped = value;
                OnPropertyChanged("ElectricityAmountTyped");
            }
        }

        private double? electricityAmount;
        public double? ElectricityAmount
        {
            get => electricityAmount;
            set
            {
                electricityAmount = value;
                if (electricityAmount == null)
                    this.ElectricityAmountTyped = false;
                else
                    this.ElectricityAmountTyped = true;
                ValidateElectricityAmount();
                OnPropertyChanged("ElectricityAmount");

            }
        }
        private string electricityAmountError;
        public string ElectricityAmountError
        {
            get => electricityAmountError;
            set
            {
                electricityAmountError = value;
                OnPropertyChanged("ElectricityAmountError");

            }
        }
        private bool ValidateElectricityAmount()
        {
            this.ShowElectricityAmountError = string.IsNullOrEmpty(ElectricityAmount.ToString());
            if (!this.ShowElectricityAmountError)
            {
                this.ElectricityAmountTyped = true;

                if (ElectricityAmount < 0)
                {
                    this.ShowElectricityAmountError = true;
                    this.ElectricityAmountError = ERROR_MESSAGES.BAD_ELECTRICITY_PAID;
                    return false;
                }
                else
                {
                    this.ShowElectricityAmountError = false;
                    return true;
                }
            }
            else
            {
                this.ElectricityAmountError = ERROR_MESSAGES.REQUIRED_FIELD;
                ElectricityAmountTyped = false;
                return false;
            }

        }


        #endregion


        #region WorkDistance 

        private bool showWorkDistanceError;

        public bool ShowWorkDistanceError
        {
            get => showWorkDistanceError;
            set
            {
                showWorkDistanceError = value;
                OnPropertyChanged("ShowWorkDistanceError");
            }
        }
        private bool workDistanceTyped;
        public bool WorkDistanceTyped
        {
            get => workDistanceTyped;
            set
            {
                workDistanceTyped = value;
                OnPropertyChanged("WorkDistanceTyped");
            }
        }

        private double? workDistance;
        public double? WorkDistance
        {
            get => workDistance;
            set
            {
                workDistance = value;
                if (string.IsNullOrEmpty(WorkDistance.ToString()))
                    this.workDistanceTyped = false;
                else
                    this.workDistanceTyped = true;
                ValidateWorkDistance();
                OnPropertyChanged("WorkDistance");

            }
        }
        private string workDistanceError;
        public string WorkDistanceError
        {
            get => workDistanceError;
            set
            {
                workDistanceError = value;
                OnPropertyChanged("WorkDistanceError");

            }
        }
        private bool ValidateWorkDistance()
        {
            this.ShowWorkDistanceError = string.IsNullOrEmpty(WorkDistance.ToString());
            if (!this.ShowWorkDistanceError)
            {
                this.WorkDistanceTyped = true;

                if (WorkDistance< 0)
                {
                    this.ShowWorkDistanceError = true;
                    this.WorkDistanceError = ERROR_MESSAGES.BAD_WORKDISTANCE;
                    return false;
                }
                else
                {
                    this.ShowWorkDistanceError = false;
                    return true;
                }
            }
            else
            {
                this.WorkDistanceError = ERROR_MESSAGES.REQUIRED_FIELD;
                WorkDistanceTyped = false;
                return false;
            }

        }


        #endregion


        #region transportation
        private string transportation;
        public string Transportation
        {
            get
            {
                return this.transportation;
            }
            set
            {
                this.transportation = value;
                OnPropertyChanged("Transportation");
            }
        }
        #endregion

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

        private bool Validate()
        {

            return ValidateMeatMeals() && ValidateElectricityAmount() && ValidateWorkDistance() && ValidatePeopleAtTheSameHouseHold();
        }
        public ICommand CalcFp => new Command(CalcFpAsync);
        private async void CalcFpAsync()
        {
            if (Validate())
            {
                
                RegularUser.DistanceToWork = (double)this.WorkDistance;
                RegularUser.InitialMeatsMeals = (int)this.MeatMeals;
                RegularUser.LastElectricityBill = (double)this.ElectricityAmount;
                RegularUser.PeopleAtTheHousehold = (int)this.PeopleAtTheSameHouseHold;
                RegularUser.Transportation = this.Transportation;
                RegularUser.Vegetarian = this.Vegetarian;
                RegularUser.VeganRareMeat = this.Vegan;

               
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                App a = (App)App.Current;
                //Loading l = new Loading();
                //l.Title = "Loading";
                //await App.Current.MainPage.Navigation.PushAsync(l);
                try
                {
                    RegularUser registeredUser = await proxy.RegisterRegularUser(RegularUser);
                    if (registeredUser == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Error","Registeration failed. Please check fields are filled as needed", "OK");
                    }
                    else
                    {
                        a.CurrentRegularUser = registeredUser;
                        a.CurrentUser = registeredUser.UserNameNavigation;
                        PopupPage popUp = new CarbonFootprintPopUp(); 

                        PopupNavigation.Instance.PushAsync(popUp);

                        Home h = new Home();
                        h.Title = "Home";
                        await App.Current.MainPage.Navigation.PushAsync(h);
                    }
                    

                }
                catch (Exception e)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Registeration failed. Please check fields are filled as needed", "OK");

                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Registeration failed. Please check fields are filled as needed", "OK");
            }

        }


    }
}
