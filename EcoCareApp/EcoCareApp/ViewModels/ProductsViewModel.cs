using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using EcoCareApp.Models;
using EcoCareApp.Services;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class ProductsViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public ProductsViewModel()
        {
            this.SearchTerm = string.Empty;

        }
        
        public ObservableCollection<Product> ProductsList
        {
            get;set;
        }


        #region Product 
        

        private async void InitProducts()
        {
            isRefreshing = true;
            App theApp = (App)App.Current;
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            List<Product> products = await proxy.GetProductsAsync();

            this.allProductsList = new ObservableCollection<Product>(); 
            foreach (Product p in products)
            {
                this.allProductsList.Add(p);
            }


            this.FilteredProducts = this.allProductsList;
            SearchTerm = string.Empty;
            IsRefreshing = false;

        }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get
            {
                return this.selectedProduct;
            }
            set
            {
                this.selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        private ObservableCollection<Product> allProductsList;
        public ObservableCollection<Product> AllProductsList
        {
            get
            {
                return allProductsList;
            }
            set
            {
                allProductsList = value;
                OnPropertyChanged("AllProductsList");
            }
        }
        private ObservableCollection<Product> filteredProducts;
        public ObservableCollection<Product> FilteredProducts
        {
            get
            {
                return this.filteredProducts;
            }
            set
            {
                if (this.filteredProducts != value)
                {
                    this.filteredProducts = value;
                    OnPropertyChanged("FilteredProducts");
                }
            }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return this.searchTerm;
            }
            set
            {
                if (this.searchTerm != value)
                {
                    this.searchTerm = value;
                    OnTextChanged(value);
                    OnPropertyChanged("SearchTerm");

                }
            }
        }

        #region Search
        public void OnTextChanged(string search)
        {
            //Filter the list of countries based on the search term
            if (this.AllProductsList == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                foreach (Product c in this.AllProductsList)
                {
                    if (!this.FilteredProducts.Contains(c))
                        this.FilteredProducts.Add(c);


                }
            }
            else
            {
                search = search.ToLower();
                foreach (Product c in this.AllProductsList)
                {
                    string ProductNameString = $"{c.Title.ToLower()}";



                    if (!this.FilteredProducts.Contains(c) &&
                       ProductNameString.Contains(search))
                        this.FilteredProducts.Add(c);
                    else if (this.FilteredProducts.Contains(c) &&
                        !ProductNameString.Contains(search))
                        this.FilteredProducts.Remove(c);
                }
            }

            this.FilteredProducts = new ObservableCollection<Product>(this.FilteredProducts);
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
        public ICommand RefreshCommand => new Command(OnRefresh);
        public void OnRefresh()
        {
            InitProducts();
        }
        #endregion


        #endregion

    }
}
