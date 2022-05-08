using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System;
using System.Linq;
using EcoCareApp.Models;
using Syncfusion.SfChart.XForms;
using EcoCareApp.Services;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using EcoCareApp.Views;

namespace EcoCareApp.ViewModels
{
    class GraphsViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    
        //users graphs
        public List<GraphItem> UserData { get; set; }
        public List<Color> Colors { get; set; }


        public Task<List<GraphItem>> GetItems()
        {

            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            return proxy.GetUserGraphsDataAsync();
        }
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
            GetItems();
        }
        #endregion
        public GraphsViewModel()
        {

            //Page p = new Loading();
            
            //UserData = GetItems().Result;
            Colors = new List<Color>()
            {
                new Color(82,182,154),
                new Color(82,182,154),

            };

            //        public Chart ExampleChart
            //        {
            //            get
            //            {
            //                var f = new List<float>();
            //                f.Add(100);
            //                this.ExampleChart.Entries = f;
            //                Chart c = new LineChart
            //                {
            //                    Entries = f,
            //                    LineMode = LineMode.Straight,
            //                    LineSize = 8,
            //                    PointMode = PointMode.Square,
            //                    PointSize = 18,

            //                };
            //                return c;
            //            }
            //            set
            //            {

            //            }
            //        }
            //        var entries = new[]
            //        {
            //                new Entry(212)
            //             {
            //                 Label = "UWP",
            //                 ValueLabel = "212",
            //                 Color = SKColor.Parse("#2c3e50")
            //             },
            //             new Entry(248)
            //             {
            //                 Label = "Android",
            //                 ValueLabel = "248",
            //                 Color = SKColor.Parse("#77d065")
            //             },
            //             new Entry(128)
            //             {
            //                 Label = "iOS",
            //                 ValueLabel = "128",
            //                 Color = SKColor.Parse("#b455b6")
            //             },
            //             new Entry(514)
            //             {
            //                 Label = "Shared",
            //                 ValueLabel = "514",
            //                 Color = SKColor.Parse("#3498db")
            //} }; 

        }
    }
}
