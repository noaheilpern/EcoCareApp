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

        private string categoryValue;
        public string CategoryValue {
            get
            {
                return categoryValue;
            }
            
            set
            {
                this.categoryValue = value;
                OnPropertyChanged("CategoryValue");
            }
        }

        public string CategoryError { get; set; }

    }
}
