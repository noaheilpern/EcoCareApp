using EcoCareApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EcoCareApp.ViewModels
{
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
                MeatMealsTyped = value;
                OnPropertyChanged("MeatMealsTyped");
            }
        }
        private string meatMeals;
        public string MeatMeals
        {
            get => meatMeals;
            set
            {
                MeatMeals = value;
                if (string.IsNullOrEmpty(MeatMeals))
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
                MeatMealsError = value;
                OnPropertyChanged("MeatMealsError");

            }
        }
        private bool ValidateMeatMeals()
        {
            this.ShowMeatMealsError = string.IsNullOrEmpty(MeatMeals.ToString());
            if (!this.ShowMeatMealsError)
            {
                this.MeatMealsTyped = true;

                if (MeatMeals.Length < MIN_MEAT_MEALS)
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

        private double electricityPaid; 
        public double ElectricityPaid
        {
            get
            {
                return this.electricityPaid;
            }
            set
            {
                this.electricityPaid = value;
                OnPropertyChanged("ElectricityPaid");
            }
        }
        
        private double workDistance;
        public double WorkDistance
        {
            get
            {
                return this.workDistance;
            }
            set
            {
                this.workDistance = value;
                OnPropertyChanged("WorkDistance");
            }
        }


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
    }
}
