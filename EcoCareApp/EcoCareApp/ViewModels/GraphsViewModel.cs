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
            this.chartType = 1;
            userData = new List<GraphItem>();
            Page p = new Loading();
            Colors = new List<Color>()
            {
                new Color(82,182,154),
                new Color(82,182,154),

            };
            InitChart();
             

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
        

        private async void InitChart()
        {
            //get data
            List<GraphItem> userData = await GetItems();

            //create chart
            Chart chart;
            switch (this.chartType % 7)
            {
                case 1:
                    chart = new LineChart();
                    break;
                case 2:
                    chart = new PieChart();
                    break;
                case 3:
                    chart = new BarChart();
                    break;
                case 4:
                    chart = new PointChart();
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
                    Label = $"{g.DateGraph}",
                    ValueLabel = $"{g.ValueFootPrint:N0}"
                };
                chartEntries.Add(entry);
            }
            chart.Entries = chartEntries;
            chart.LabelTextSize += 10;

            MainChart = chart;
        }

        public ICommand NextChart => new Command(OnNextChart);
        public void OnNextChart()
        {
            this.chartType++;
            InitChart();
        }

    }
}
