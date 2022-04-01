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
    public partial class EditProduct : ContentPage
    {
        public EditProduct()
        {
            InitializeComponent();
            this.BindingContext = new EditProductViewModel();
        }
    }
}