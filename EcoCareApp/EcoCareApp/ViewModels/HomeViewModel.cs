﻿using EcoCareApp.Services;
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

        public bool visibleLine;
        public bool VisibleLine
        {
            get
            { return visibleLine; }
            set
            {
                visibleLine = value;
                OnPropertyChanged("VisibleLine");
            }
        }

        public bool dataFilled;
        public bool DataFilled
        {
            get
            { return dataFilled; }
            set
            {
                dataFilled = value;
                OnPropertyChanged("DataFilled");
            }
        }
        public ICommand SomethingPressed => new Command(CirclePressed);
        void CirclePressed()
        {
            Visible = true;
        }

        public ICommand ElecCommand => new Command(ElecPressed);
        async void ElecPressed()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            if(await proxy.IsDataExist("Electricity_Usage"))
            {
                DataFilled = true;
                Visible = false;
            }
            else
            {
                ElecTapped = true;
                Visible = true;
                DataFilled = false; 
            }
            CarTapped = false;
            MeatTapped = false;
            VisibleLine = true; 
        }

        public ICommand CarCommand => new Command(CarPressed);
        async void CarPressed()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            if (await proxy.IsDataExist("Distance"))
            {
                DataFilled = true;
                Visible = false;
            }
            else
            {
                DataFilled = false; 
                CarTapped = true;
                Visible = true;
            }
            ElecTapped = false;
            MeatTapped = false;
            VisibleLine = true;

        }

        public ICommand MeatCommand => new Command(MeatPressed);
        async void MeatPressed()
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            if (await proxy.IsDataExist("Meat_Meals"))
            {
                DataFilled = true;
                Visible = false; 
            }
            else
            {
                DataFilled = false; 
                MeatTapped = true;
                Visible = true;
            }
            CarTapped = false;
            ElecTapped = false;
            VisibleLine = true;

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

