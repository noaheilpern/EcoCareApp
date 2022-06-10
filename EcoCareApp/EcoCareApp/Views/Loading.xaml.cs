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
    public partial class Loading : ContentPage
    {
        public Loading()
        {
            this.BindingContext = new LoadingViewModel();
            InitializeComponent(); 
        }
        public Loading(Object bindingContext)
        {
            this.BindingContext = bindingContext; 
            InitializeComponent();
        }
    }
}