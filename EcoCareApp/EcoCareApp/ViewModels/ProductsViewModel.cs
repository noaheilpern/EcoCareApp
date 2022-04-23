using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EcoCareApp.Models;
using EcoCareApp.Services;
using EcoCareApp.Views;
using Syncfusion.ListView.XForms;
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

        public Command<ItemSelectionChangedEventArgs> selectionChangedCommand;

        public Command<ItemSelectionChangedEventArgs> SelectionChangedCommand
        {
            get { return selectionChangedCommand; }
            set { selectionChangedCommand = value; }
        }


        private void OnSelectionChanged(ItemSelectionChangedEventArgs obj)
        {
            App.Current.MainPage.DisplayAlert("Alert", (obj.AddedItems[0] as Product).Title + " is selected", "OK");
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
                if(theApp.CurrentSeller!=null)
                {
                    if(p.SellersUsername.Equals(theApp.CurrentSeller.UserName))
                    {
                        if (p.Active == true)
                            this.allProductsList.Add(p);
                    }
                }
                else
                {
                    if (p.Active == true)
                        this.allProductsList.Add(p);
                }
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
        public ICommand DeleteItem  => new Command<Product>(Delete);
        public async void Delete(Product p)
        {
            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            await proxy.DeleteItemAsync(p);
            allProductsList.Remove(p);
            filteredProducts.Remove(p); 
            
        }
        public ICommand RefreshCommand => new Command(OnRefresh);
        public void OnRefresh()
        {
            InitProducts();
        }
        public ICommand AddProductPage => new Command(ToAdd);
        public async void ToAdd()
        {
            App a = (App)App.Current;

            await App.Current.MainPage.Navigation.PushAsync(new AddProduct());
        }
        public ICommand EditItem => new Command<Product>(ToEditPage);
        public async void ToEditPage(Product obj)
        {
            if (obj is Product)
            {

                Product chosenProduct = (Product)obj;
                EditProductViewModel ProductContext = new EditProductViewModel
                {
                    Title = chosenProduct.Title,
                    Active = chosenProduct.Active,
                    Description = chosenProduct.Description,
                    ImageSource = chosenProduct.ImageSource,
                    Price = chosenProduct.Price,
                    SellersUsername = chosenProduct.SellersUsername,
                    ProductId = chosenProduct.ProductId,
                    

                };
                
                Page showProduct = new EditProduct();


                showProduct.BindingContext = ProductContext;
                showProduct.Title = ProductContext.Title;
                App a = (App)App.Current;

                await App.Current.MainPage.Navigation.PushAsync(showProduct);

            }
        }
        
        public ICommand SwipeStartedCommand => new Command(SwipeStarted);
        public void SwipeStarted()
        {

        }
        public ICommand ProductSelected => new Command(ToProductPage);

        public async void ToProductPage(Object obj)
        {
            if (obj is Product)
            {
                Product chosenProduct = (Product)obj;
                ProductPageViewModel ProductContext = new ProductPageViewModel
                {
                    Title = chosenProduct.Title,
                    Active = chosenProduct.Active,
                    Description = chosenProduct.Description,
                    Price = chosenProduct.Price,
                    ImageSource = chosenProduct.ImageSource,
                    SellersUsername = chosenProduct.SellersUsername,
                    ProductId = chosenProduct.ProductId,

                };
                Page showProduct = new ProductPage(ProductContext);


                showProduct.BindingContext = ProductContext;
                showProduct.Title = ProductContext.Title;
                App a = (App)App.Current;
               
                await App.Current.MainPage.Navigation.PushAsync(showProduct);

            }
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
            InitProducts();

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

        #region Events
        //Events
        //This event is used to navigate to the monkey page
        public Action<Page> NavigateToPageEvent;
        #endregion
    }

}

