using EcoCareApp.Models;
using EcoCareApp.Services;
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

        public EditProductViewModel()
        {
           

        }
        public int ProductId { get; set; }
        public string SellersUsername { get; set; }
        public int Price { get; set; }
        public Product Product { get; set; }
        private bool active;
        public bool Active { get
            {
                return this.active;
            }
            set
            {
                this.active = value;
                OnPropertyChanged("Active");
            }
        }

        #region Description
        private bool showDescriptionError;

        public bool ShowDescriptionError
        {
            get => showDescriptionError;
            set
            {
                showDescriptionError = value;
                OnPropertyChanged("ShowDescriptionError");
            }
        }

        public bool descriptionTyped;
        public bool DescriptionTyped
        {
            get => descriptionTyped;
            set
            {
                descriptionTyped = value;
                OnPropertyChanged("DescriptionTyped");
            }
        }
        private string descriptionError;

        public string DescriptionError
        {

            get => descriptionError;
            set
            {
                descriptionError = value;
                OnPropertyChanged("DescriptionError");
            }

        }


        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;

                this.ShowDescriptionError = string.IsNullOrEmpty(Description) || this.Description.Length > 8000;
                if (ShowDescriptionError)
                {
                    this.DescriptionTyped = false;
                    if(string.IsNullOrEmpty(Description))
                    {
                        this.DescriptionError = ERROR_MESSAGES.REQUIRED_FIELD;

                    }
                    else
                    {
                        this.DescriptionError = "Field must be less than 8,000 charecters";
                    }
                    OnPropertyChanged("Description");
                }
                else
                {
                    this.DescriptionTyped = true;
                    this.ShowDescriptionError = false;
                }

            }
        }



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

                this.ShowTitleError = string.IsNullOrEmpty(Title)|| this.Title.Length>250;
                if (ShowTitleError)
                {
                    this.TitleTyped = false;

                    if (string.IsNullOrEmpty(Title))
                    {
                        this.TitleError = ERROR_MESSAGES.REQUIRED_FIELD;
                    }
                    else
                    {
                        this.TitleError = "Field must be less than 255 charecters";
                    }
                   
                    OnPropertyChanged("Title");
                }
                else
                {
                    this.TitleTyped = true;
                    this.ShowTitleError = false;
                }

            }
        }



        #endregion

        #region imageSource
        private bool showImageSourceError;

        public bool ShowImageSourceError
        {
            get => showImageSourceError;
            set
            {
                showImageSourceError = value;
                OnPropertyChanged("ShowImageSourceError");
            }
        }

        public bool imageSourceTyped;
        public bool ImageSourceTyped
        {
            get => imageSourceTyped;
            set
            {
                imageSourceTyped = value;
                OnPropertyChanged("ImageSourceTyped");
            }
        }
        private string imageSourceError;

        public string ImageSourceError
        {

            get => imageSourceError;
            set
            {
                imageSourceError = value;
                OnPropertyChanged("ImageSourceError");
            }

        }


        private string imageSource;
        public string ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;

                this.ShowImageSourceError = string.IsNullOrEmpty(imageSource) || this.imageSource.Length > 250;
                if (ShowImageSourceError)
                {
                    this.ImageSourceTyped = false;

                    if (string.IsNullOrEmpty(ImageSource))
                    {
                        this.ImageSourceError = ERROR_MESSAGES.REQUIRED_FIELD;
                    }
                    else
                    {
                        this.ImageSourceError = "Field must be less than 255 charecters";
                    }

                    OnPropertyChanged("ImageSource");
                }
                else
                {
                    this.ImageSourceTyped = true;
                    this.ShowImageSourceError = false;
                }

            }
        }



        #endregion

        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        #endregion
        #region Commands

        public ICommand Update => new Command(UpdateProductAsync);



        private async void UpdateProductAsync()
        {
            Product = new Product
            {
                Title = this.Title,
                Description = this.Description,
                Active = this.active,
                ImageSource = this.ImageSource,
                Price = this.Price,
                SellersUsername = this.SellersUsername,
                ProductId = this.ProductId,
            };
            bool worked = true;
            App a = (App)App.Current;
            Product product = Product;

            
            product.Title = this.Title;
            product.Description = this.Description;
            product.ImageSource = this.ImageSource;

            App app = (App)App.Current;
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();

            isRefreshing = true; 
            worked = await proxy.UpdateProductAsync(product);
            isRefreshing = false;
                

            
            if (!worked)
            {
              await App.Current.MainPage.DisplayAlert("Error", "Updating product data failed. Please check fields are filled as needed", "OK");

            }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Succeed", "detail(s) updated successfully", "great:)");

                }
        }
   
        #endregion
    }
}

    

