using EcoCareApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EcoCareApp.ViewModels
{
    public static partial class ERROR_MESSAGES
    {
        public const string BAD_MEATMEALS = "You can't eat less than zero meat meals per week";
        public const string BAD_ELECTRICITY_PAID = "You can't pay less than 0" +
            " dollars to the electricity company";
        public const string BAD_WORKDISTANCE = "Thw work distance cna't be less than 0";
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
        private int meatMeals;
        public int MeatMeals
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

        #region ElectricityPaid 

        private bool showElectricityPaidError;

        public bool ShowElectricityPaidError
        {
            get => showElectricityPaidError;
            set
            {
                showElectricityPaidError = value;
                OnPropertyChanged("ShowElectricityPaidError");
            }
        }
        private bool electricityPaidTyped;
        public bool ElectricityPaidTyped
        {
            get => electricityPaidTyped;
            set
            {
                electricityPaidTyped = value;
                OnPropertyChanged("ElectricityPaidTyped");
            }
        }

        private double electricityPaid;
        public double ElectricityPaid
        {
            get => electricityPaid;
            set
            {
                electricityPaid = value;
                if (string.IsNullOrEmpty(ElectricityPaid.ToString()))
                    this.ElectricityPaidTyped = false;
                else
                    this.ElectricityPaidTyped = true;
                ValidateElectricityPaid();
                OnPropertyChanged("ElectricityPaid");

            }
        }
        private string electricityPaidError;
        public string ElectricityPaidError
        {
            get => electricityPaidError;
            set
            {
                electricityPaidError = value;
                OnPropertyChanged("ElectricityPaidError");

            }
        }
        private bool ValidateElectricityPaid()
        {
            this.ShowElectricityPaidError = string.IsNullOrEmpty(ElectricityPaid.ToString());
            if (!this.ShowElectricityPaidError)
            {
                this.ElectricityPaidTyped = true;

                if (electricityPaid < 0)
                {
                    this.ShowElectricityPaidError = true;
                    this.ElectricityPaidError = ERROR_MESSAGES.BAD_ELECTRICITY_PAID;
                    return false;
                }
                else
                {
                    this.ShowElectricityPaidError = false;
                    return true;
                }
            }
            else
            {
                this.ElectricityPaidError = ERROR_MESSAGES.REQUIRED_FIELD;
                ElectricityPaidTyped = false;
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

        private double workDistance;
        public double WorkDistance
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
    }
}
