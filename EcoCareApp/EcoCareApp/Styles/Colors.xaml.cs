using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcoCareApp.Style
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Colors : ContentPage
    {
        public Colors()
        {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(Colors));
        }
    }
}