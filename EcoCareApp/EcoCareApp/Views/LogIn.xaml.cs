using EcoCareApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcoCareApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn : ContentPage
    {
        public LogIn()
        {
            LogInViewModel context = new LogInViewModel();
            //Register to the event so the view model will be able to navigate to the monkeypage
            this.BindingContext = context;

            InitializeComponent();
        }
        //Allow ViewModel to call this function if needed to navigate to another page!
        public async void NavigateToAsync(Page p)
        {
            await Navigation.PushAsync(p);
        }

    }
}