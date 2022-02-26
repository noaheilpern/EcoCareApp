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

        public double MeatText { get; set; }
        public ICommand Save => new Command(SaveData);

        async void SaveData()
        {
            try
            {
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();

                if (ElecTapped)
                {
                    await proxy.AddData(6,"Electricity");
                }
                else if (CarTapped)
                {

                }
                else
                {
                    await proxy.AddData(MeatText, "Meat_Meals");
                }

            }
            catch(Exception e)
            {
            }


        }
    }
}

