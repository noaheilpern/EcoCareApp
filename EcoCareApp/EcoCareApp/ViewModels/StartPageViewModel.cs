using EcoCareApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class StartPageViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public Action<Page> NavigateToPageEvent;

        public ICommand ToRegisterOwner => new Command(ToRegisterO);
        void ToRegisterO()
        {
            Page p = new RegisterOwner();
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);
        }
        
        public ICommand ToRegisterUser => new Command(ToRegisterU);
        void ToRegisterU()
        {
            Page p = new RegisterUser();
            if (NavigateToPageEvent != null)
                NavigateToPageEvent(p);
        }

    }
}
