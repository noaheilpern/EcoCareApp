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
            if (allProductsList.Count == 0)
            {
                App a = (App)App.Current;
                if(a.CurrentSeller== null)
                {
                    NoProductsToSeller = false;
                    NoProductsToUser = true; 
                }
                else
                {
                    NoProductsToSeller = true;
                    NoProductsToUser = false;
                }
                    
            }
            else
            {
                NoProductsToSeller = false;
                NoProductsToUser = false;
            }
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
                List<Product> products = new List<Product>();
                foreach (Product p in this.allProductsList)
                    products.Add(p);
                foreach (Product c in products)
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
            //showProduct.Title = ProductContext.Title;
            
        }
        public ICommand RefreshCommand => new Command(OnRefresh);
        public async void OnRefresh()
        {
            InitProducts();
            if (IsRegular)
            {
                App a = (App)App.Current;
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                
                a.CurrentRegularUser = await proxy.GetRegularUserDataAsync(a.CurrentUser.UserName);
                app.Stars = (int)a.CurrentRegularUser.Stars; 
            }

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
                App a = (App)App.Current; 
                
                if(a.CurrentRegularUser != null)
                {
                    ProductContext.Stars = (int)a.CurrentRegularUser.Stars;

                    if (a.CurrentRegularUser.Stars < ProductContext.Price)
                    {
                        ProductContext.HasEnoughStars = false;
                        ProductContext.HasNotEnoughStars = true;
                    }
                    else
                    {
                        ProductContext.HasEnoughStars = true;
                        ProductContext.HasNotEnoughStars = false;
                    }


                }

                Page showProduct = new ProductPage(ProductContext);


                showProduct.BindingContext = ProductContext;
                showProduct.Title = ProductContext.Title;
                
                await App.Current.MainPage.Navigation.PushAsync(showProduct);

            }
        }

        #endregion


        #endregion

        public App app { get; } = Application.Current as App;

        public bool IsRegular { get; set; }

        public bool IsSeller { get; set; }

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
        /// 
        public ProductsViewModel()
        {
            this.SearchTerm = string.Empty;
            InitProducts();
            
            App a = (App)App.Current;
            if (a.CurrentSeller != null)
            {
                IsRegular = false;
                IsSeller = true;

            }
            else
            {
                IsRegular = true; 
                IsSeller = false;
                app.Stars = a.Stars;
            }
        }

        #endregion

        #region EmptyCases 
        bool noProductsToUser;

        public bool NoProductsToUser {
            get=> noProductsToUser; 
            set
            {
                noProductsToUser = value; 
                OnPropertyChanged("NoProductsToUser");
            } 
        }
        bool noProductsToSeller;
        public bool NoProductsToSeller
        {
            get => noProductsToSeller;
            set
            {
                noProductsToSeller = value;
                OnPropertyChanged("NoProductsToSeller");
            }
        }
        #endregion


        #region Events
        //Events
        //This event is used to navigate to the monkey page
        public Action<Page> NavigateToPageEvent;
        #endregion
    }

}

