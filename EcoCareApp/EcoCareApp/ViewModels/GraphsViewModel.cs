﻿using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System;
using System.Linq;
using EcoCareApp.Models;

namespace EcoCareApp.ViewModels
{
    class GraphsViewModel
    {

        //users graphs
        public List<DataItem> UserData { get; set; }

        public GraphsViewModel()
        {
            //user data - פעולה בשרת
            UserData = new List<DataItem>()
            {
                new DataItem {Value = 30, Date = DateTime.Today},
                new DataItem { Value = 70, Date = DateTime.Today},
            };
            
        }


        
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
