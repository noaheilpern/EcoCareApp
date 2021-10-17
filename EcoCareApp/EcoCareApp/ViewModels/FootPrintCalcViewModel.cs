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
        
        private int meatMeals;
        public int MeatMeals
        {
            get
            {
                return this.meatMeals;
            }
            set
            {
                this.meatMeals = value;
                OnPropertyChanged("MeatMeals");
            }
        }

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
