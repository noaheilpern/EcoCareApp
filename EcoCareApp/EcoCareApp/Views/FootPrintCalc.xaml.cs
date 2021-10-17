using EcoCareApp.Models;
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
    public partial class FootPrintCalc : ContentPage
    {
        public FootPrintCalc(RegularUser ru)
        {
            this.BindingContext = new FootPrintCalcViewModel(ru); 
            InitializeComponent();
        }
    }
}