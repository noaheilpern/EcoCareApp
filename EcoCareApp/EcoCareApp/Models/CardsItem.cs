using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace EcoCareApp.Models
{
    class CardsItem: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string category;
        private string categoryValue;
        private string categoryError;

        // Gets or sets the item's category.
        public string Category
        {
            get => category;

            set
            {
                category = value;
                OnPropertyChanged("Category");
            }
        }

        // Gets or sets the item's category value.
        public string CategoryValue
        {
            get => categoryValue;

            set
            {
                categoryValue = value;
                OnPropertyChanged("CategoryValue");
                ValidateCmd?.Execute(null); // validation
            }
        }

        // Gets or sets the (paramter-less) validation command, that executed when category's value is
        // changed.
        public ICommand ValidateCmd { get; set; }

        // Gets or sets the item's error (if any). Null value indicates no error exists.
        public string CategoryError
        {
            get => categoryError;

            set
            {
                categoryError = value;
                OnPropertyChanged("CategoryError");
                OnPropertyChanged("IsError");
            }
        }

        // Returns whether the item's has an error
        public bool IsError => categoryError != null;

    }
}
