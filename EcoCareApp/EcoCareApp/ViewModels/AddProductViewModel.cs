using EcoCareApp.Models;
using EcoCareApp.Services;
using EcoCareApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class AddProductViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

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

        private bool ValidateDescription()
        {
            this.ShowDescriptionError = string.IsNullOrEmpty(Description) || this.Description.Length > 8000;
            if (ShowDescriptionError)
            {
                this.DescriptionTyped = false;
                if (string.IsNullOrEmpty(Description))
                {
                    this.DescriptionError = ERROR_MESSAGES.REQUIRED_FIELD;

                }
                else
                {
                    this.DescriptionError = "Field must be less than 8,000 charecters";
                }
                OnPropertyChanged("Description");
                return false; 
            }
            else
            {
                this.DescriptionTyped = true;
                this.ShowDescriptionError = false;
                return true; 
            }
        }
        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;

                

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

                this.ShowTitleError = string.IsNullOrEmpty(Title) || this.Title.Length > 250;
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

        #region Price
        private bool showPriceError;

        public bool ShowPriceError
        {
            get => showPriceError;
            set
            {
                showPriceError = value;
                OnPropertyChanged("ShowPriceError");
            }
        }

        public bool priceTyped;
        public bool PriceTyped
        {
            get => priceTyped;
            set
            {
                priceTyped = value;
                OnPropertyChanged("PriceTyped");
            }
        }
        private string priceError;

        public string PriceError
        {

            get => priceError;
            set
            {
                priceError = value;
                OnPropertyChanged("PriceError");
            }

        }


        private int price;
        public int Price
        {
            get => price;
            set
            {
                price = value;

                if(Price <=0)
                {
                    this.ShowPriceError = true;             
                    this.PriceTyped = true;
                    this.PriceError = "Price must be more than 0";
                    OnPropertyChanged("Price");
                }
                else
                {
                    this.PriceTyped = true;
                    this.ShowPriceError = false;
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

        public bool Validate()
        {
            if (string.IsNullOrEmpty(Title))
            {
                TitleError = ERROR_MESSAGES.REQUIRED_FIELD;
                ShowTitleError = true;
                return false;
            }
            bool valid = ValidateDescription();
            if (string.IsNullOrEmpty(ImageSource))
            {
                ImageSourceError = ERROR_MESSAGES.REQUIRED_FIELD;
                ShowImageSourceError = true;
                return false;
            }
            if (Price == 0)
            {
                PriceError = ERROR_MESSAGES.REQUIRED_FIELD;
                ShowPriceError = true;
                return false;
            }
            return true; 
        }

        #region Commands
        

        public ICommand AddCommand => new Command(AddProduct);



        private async void AddProduct()
        {
            if(Validate())
            {
                App a = (App)App.Current;
                if (a.CurrentSeller == null)
                    return;
                Product returnedProduct = null;
                App app = (App)App.Current;
                Product p = new Product
                {

                    Title = this.Title,
                    Description = this.Description,
                    Active = true,
                    ImageSource = this.ImageSource,
                    Price = this.Price,
                    SellersUsername = a.CurrentSeller.UserName,

                };
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();

                isRefreshing = true;
                returnedProduct = await proxy.AddProductAsync(p);
                isRefreshing = false;


                if (returnedProduct == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Updating product data failed. Please check fields are filled as needed", "OK");

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Succeed", "detail(s) updated successfully", "great:)");

                    ProductPageViewModel ProductContext = new ProductPageViewModel
                    {
                        Title = returnedProduct.Title,
                        Active = returnedProduct.Active,
                        Description = returnedProduct.Description,
                        Price = returnedProduct.Price,
                        ImageSource = returnedProduct.ImageSource,
                        SellersUsername = returnedProduct.SellersUsername,
                        ProductId = returnedProduct.ProductId,

                    };
                    Page showProduct = new ProductPage(ProductContext);


                    showProduct.BindingContext = ProductContext;
                    showProduct.Title = ProductContext.Title;

                    await App.Current.MainPage.Navigation.PushAsync(showProduct);

                }
            }
            
            }

        #endregion

    }
}
