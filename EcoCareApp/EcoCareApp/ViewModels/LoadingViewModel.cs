using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EcoCareApp.ViewModels
{
    class LoadingViewModel: INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public LoadingViewModel()
        {
            ServerStatus = "Connecting to the server...";
        }
        private string serverStatus; 
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
    }
}
