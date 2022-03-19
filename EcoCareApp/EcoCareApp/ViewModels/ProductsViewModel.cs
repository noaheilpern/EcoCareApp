using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
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

        #region Fields


        private ObservableCollection<string> sortOptions;

        private Command addFavouriteCommand;

        private Command itemSelectedCommand;

        private Command sortCommand;

        private Command filterCommand;

        private Command addToCartCommand;

        private Command cardItemCommand;

        private string cartItemCount;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="CatalogPageViewModel" /> class.
        /// </summary>
        public ProductsViewModel()
        {
            this.SearchTerm = string.Empty;

        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the item details in tile.
        /// </summary>
        [DataMember(Name = "products")]
        public ObservableCollection<Product> Products
        {
            get; set;
        }


        /// <summary>
        /// Gets or sets the property has been bound with a list view, which displays the sort details.
        /// </summary>
        public ObservableCollection<string> SortOptions
        {
            get
            {
                return this.sortOptions;
            }

            private set
            {
                if (this.sortOptions == value)
                {
                    return;
                }

                this.SetProperty(ref this.sortOptions, value);
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the cart items count.
        /// </summary>
        public string CartItemCount
        {
            get
            {
                return this.cartItemCount;
            }

            set
            {
                this.SetProperty(ref this.cartItemCount, value);
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand
        {
            get { return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command(this.ItemSelected)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the sort button is clicked.
        /// </summary>
        public Command SortCommand
        {
            get { return this.sortCommand ?? (this.sortCommand = new Command(this.SortClicked)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the filter button is clicked.
        /// </summary>
        public Command FilterCommand
        {
            get { return this.filterCommand ?? (this.filterCommand = new Command(this.FilterClicked)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the Favourite button is clicked.
        /// </summary>
        public Command AddFavouriteCommand
        {
            get
            {
                return this.addFavouriteCommand ?? (this.addFavouriteCommand = new Command(this.AddFavouriteClicked));
            }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the AddToCart button is clicked.
        /// </summary>
        public Command AddToCartCommand
        {
            get { return this.addToCartCommand ?? (this.addToCartCommand = new Command(this.AddToCartClicked)); }
        }

        /// <summary>
        /// Gets or sets the command will be executed when the cart icon button has been clicked.
        /// </summary>
        public Command CardItemCommand
        {
            get { return this.cardItemCommand ?? (this.cardItemCommand = new Command(this.CartClicked)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void ItemSelected(object attachedObject)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the items are sorted.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void SortClicked(object attachedObject)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the filter button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void FilterClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the favourite button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddFavouriteClicked(object obj)
        {
            
        }

        /// <summary>
        /// Invoked when the cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddToCartClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when cart icon button is clicked.
        /// </summary>
        /// <param name="obj"></param>
        private void CartClicked(object obj)
        {
            // Do something
        }

        #endregion
    }

}

