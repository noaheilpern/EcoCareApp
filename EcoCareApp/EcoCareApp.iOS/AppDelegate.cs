using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfRadialMenu.XForms.iOS;
using Syncfusion.XForms.iOS.EffectsView;
using UIKit;

namespace EcoCareApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            SfRadialMenuRenderer.Init();
            SfListViewRenderer.Init();
            Syncfusion.XForms.iOS.Cards.SfCardViewRenderer.Init();
            Syncfusion.XForms.iOS.BadgeView.SfBadgeViewRenderer.Init();

            SfEffectsViewRenderer.Init();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            Syncfusion.XForms.iOS.PopupLayout.SfPopupLayoutRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init(); 
            //Syncfusion.SfGauge.XForms.iOS.SfDigitalGaugeRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
