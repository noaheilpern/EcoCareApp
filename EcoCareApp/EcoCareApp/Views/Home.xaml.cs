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
    public partial class Home : ContentPage
    {
        public Home()
        {
            this.BindingContext = new HomeViewModel();

            InitializeComponent();

            App app = (App)App.Current;
            if (app.CurrentSeller != null)
            {
                userHome.Content = new HomeForSeller();
            }

            mainTabs.SelectedIndex = 1;

            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if(mainTabs.TabItems[3].IsSelected)
            {
                await ((GraphsViewModel)(mainTabs.TabItems[3].Content.BindingContext)).InitChart(); 
            }
        }
    }
}