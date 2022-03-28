using EcoCareApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class EditProductViewModel : INotifyPropertyChanged
    {


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Description

        #endregion

        #region Title
        private bool showTitleError;

        public bool ShowTitleError
        {
            get => showTitleError;
            set
            {
                showTitleError = value;
                OnPropertyChanged("ShowTitleError");
            }
        }

        public bool titleTyped;
        public bool TitleTyped
        {
            get => titleTyped;
            set
            {
                titleTyped = value;
                OnPropertyChanged("TitleTyped");
            }
        }
        private string titleError;

        public string TitleError
        {

            get => titleError;
            set
            {
                titleError = value;
                OnPropertyChanged("TitleError");
            }

        }


        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;

                this.ShowTitleError = string.IsNullOrEmpty(Title);
                if (ShowTitleError)
                {
                    this.TitleTyped = false;
                    this.TitleError = ERROR_MESSAGES.REQUIRED_FIELD;
                    OnPropertyChanged("FirstName");
                }
                else
                {
                    this.TitleTyped = true;
                    this.ShowTitleError = false;
                }

            }
        }



        #endregion


    }
}

    

