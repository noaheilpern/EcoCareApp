using EcoCareApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class HomeViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private bool meatFilled;
        public bool MeatFilled
        {
            get
            { return meatFilled; }
            set
            {
                meatFilled = value;
                OnPropertyChanged("MeatFilled");
            }
        }

        private bool carFilled;
        public bool CarFilled
        {
            get
            { return carFilled; }
            set
            {
                carFilled = value;
                OnPropertyChanged("CarFilled");
            }
        }

        private bool meatTapped;
        public bool MeatTapped
        {
            get
            { return meatTapped; }
            set
            {
                meatTapped = value;
                OnPropertyChanged("MeatTapped");
            }
        }

        private bool elecFilled;
        public bool ElecFilled
        {
            get
            { return elecFilled; }
            set
            {
                elecFilled = value;
                OnPropertyChanged("ElecFilled");
            }
        }
        private bool carTapped;
        public bool CarTapped
        {
            get
            { return carTapped; }
            set
            {
                carTapped = value;
                OnPropertyChanged("CarTapped");
            }
        }

        private bool elecTapped;
        public bool ElecTapped
        {
            get
            { return elecTapped; }
            set
            {
                elecTapped = value;
                OnPropertyChanged("ElecTapped");
            }
        }


      
        public double meatEntry;
        public double MeatEntry
        {
            get
            { return meatEntry; }
            set
            {
                meatEntry = value;
                OnPropertyChanged("MeatEntry");
            }
        }

        public double elecEntry;
        public double ElecEntry
        {
            get
            { return elecEntry; }
            set
            {
                elecEntry = value;
                OnPropertyChanged("ElecEntry");
            }
        }

        public double carEntry;
        public double CarEntry
        {
            get
            { return carEntry; }
            set
            {
                carEntry = value;
                OnPropertyChanged("CarEntry");
            }
        }

        public bool visible;
        public bool Visible
        {
            get
            { return visible; }
            set
            {
                visible = value;
                OnPropertyChanged("Visible");
            }
        }

        public ICommand SomethingPressed => new Command(CirclePressed);
        void CirclePressed()
        {
            Visible = true;
        }

        public ICommand ElecCommand => new Command(ElecPressed);
        void ElecPressed()
        {
            carTapped = false;
            meatTapped = false;
            elecTapped = true;

        }

        public ICommand CarCommand => new Command(CarPressed);
        void CarPressed()
        {
            carTapped = true;
            meatTapped = false;
            elecTapped = false;

        }

        public ICommand MeatCommand => new Command(MeatPressed);
        void MeatPressed()
        {
            carTapped = false;
            meatTapped = true;
            elecTapped = false;

        }

        public ICommand Save => new Command(SaveData);

        async void SaveData()
        {
            try
            {
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();

                if (ElecTapped)
                {
                    await proxy.AddData(ElecEntry, "Electricity_Usage");
                    ElecTapped = false;
                }
                else if (CarTapped)
                {
                    await proxy.AddData(CarEntry, "Distance");
                    CarTapped = false;
                }
                else
                {
                    await proxy.AddData(MeatEntry, "Meat_Meals");
                    MeatTapped = false;
                    MeatFilled = true; 
                }

            }
            catch(Exception e)
            {

            }


        }
    }
}

