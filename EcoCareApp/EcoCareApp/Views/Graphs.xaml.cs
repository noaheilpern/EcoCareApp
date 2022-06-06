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
    public partial class Graphs : ContentView
    {
        public Action refreshGraphs;

        public Graphs()
        {
            
            this.BindingContext = new GraphsViewModel();
            refreshGraphs += ((GraphsViewModel)(this.BindingContext)).OnRefresh; 
            InitializeComponent();
            if (Device.RuntimePlatform == Device.UWP)
            {
                //axisLabelStyle.FontSize = 14;
            }

        }
        
    }
}