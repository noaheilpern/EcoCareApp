using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using EcoCareApp.Models;
using EcoCareApp.Services;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using EcoCareApp.Views;
using System.Collections.ObjectModel;

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
        private List<GraphItem> userData;
        public List<Color> Colors { get; set; }

      
        public Task<List<GraphItem>> GetItems()
        {

            EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
            App a = (App)App.Current;
            if (a.CurrentRegularUser != null)
            {
                return proxy.GetUserGraphsDataAsync();
            }
            else if (a.CurrentSeller != null)
                return proxy.GetSellerGraphsDataAsync();
            else
                //manager option
                return null; 
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

        public Action graphs; 
        public ICommand RefreshCommand => new Command(OnRefresh);
        public async void OnRefresh()
        {
            await InitChart();
            if(IsRegular)
            {
                App a = (App)App.Current;
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();

                a.CurrentRegularUser = await proxy.GetRegularUserDataAsync(a.CurrentUser.UserName);
                app.Stars = (int)a.CurrentRegularUser.Stars;
            }
            
        }
        #endregion
        public App app { get; } = Application.Current as App;

        public bool IsRegular { get; set; }
        
        public GraphsViewModel()
        {
             this.chartType = 1;
            userData = new List<GraphItem>();

            App a = (App)App.Current;
            if (a.CurrentSeller != null)
            {
                IsRegular = false;

            }
            else
            {
                IsRegular = true;

                app.Stars = a.Stars;
            }


        }



        private Chart mainChart;
        public Chart MainChart
        {
            get => this.mainChart;
            set
            {
                this.mainChart = value;
                OnPropertyChanged("MainChart");
            }
        }
        private string title;
        public string Title
        {
            get => this.title;
            set
            {
                this.title = value;
                OnPropertyChanged("Title");
            }
        }

        private int chartType;
        

        public async Task InitChart()
        {
            //get data
            List<GraphItem> userData = await GetItems();

            //create chart
            Chart chart;
            switch (this.chartType % 7)
            {
                case 1:
                    chart = new LineChart
                    {
                        LabelOrientation = Orientation.Horizontal,
                        ValueLabelOrientation = Orientation.Horizontal,

                    };
                    break;
                case 2:
                    chart = new PieChart();
                    break;
                case 3:
                    chart = new BarChart
                    {

                        LabelOrientation = Orientation.Horizontal,
                        ValueLabelOrientation = Orientation.Horizontal,
                    };
                    break;
                case 4:
                    chart = new PointChart
                    {

                        LabelOrientation = Orientation.Horizontal,
                        ValueLabelOrientation = Orientation.Horizontal,
                    };
                    break;
                case 5:
                    chart = new RadarChart();
                    break;
                case 6:
                    chart = new DonutChart();
                    break;
                default:
                    chart = new RadialGaugeChart();
                    break;
            }

            List<ChartEntry> chartEntries = new List<ChartEntry>();
            foreach (GraphItem g in userData)
            {
                ChartEntry entry = new ChartEntry((float)g.ValueFootPrint)
                {
                    TextColor = SKColor.Parse("#3498db"),
                    ValueLabelColor = SKColor.FromHsv((float)g.ValueFootPrint % 100, (float)g.ValueFootPrint % 100 / 2, (float)g.ValueFootPrint % 100 / 4),
                    Color = SKColor.FromHsv((float)g.ValueFootPrint, (float)g.ValueFootPrint / 2, (float)g.ValueFootPrint / 4),
                    Label = String.Format("{0:dd/MM/yy}", g.DateGraph),
                    ValueLabel = $"{g.ValueFootPrint:N0}"
                };
                chartEntries.Add(entry);
            }
            
            chart.Entries = chartEntries;
            chart.LabelTextSize += 10;
            
            MainChart = chart;
        }

        public ICommand NextChart => new Command(OnNextChart);
        public async void OnNextChart()
        {
            this.chartType++;
            await InitChart();
        }

    }
}
