using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
        public string Category { get; set; }
        public string CategoryValue {
            get
            {
                return CategoryValue;
            }

            set
            {
                this.CategoryValue = value;
                OnPropertyChanged("CategoryValue");
            }
        }

        public string CategoryError { get; set; }
    }
}
