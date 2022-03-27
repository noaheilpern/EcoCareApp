using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EcoCareApp.ViewModels;

namespace EcoCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Products : ContentView
    {
        public Products()
        {
            InitializeComponent();

            ProductsViewModel context = new ProductsViewModel();
            //Register to the event so the view model will be able to navigate to the monkeypage
            context.NavigateToPageEvent += NavigateToAsync;
            this.BindingContext = context;

        }
        //Allow ViewModel to call this function if needed to navigate to another page!
        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);
        }


    }
}