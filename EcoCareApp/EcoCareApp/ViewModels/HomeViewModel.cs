using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EcoCareApp.ViewModels
{
    class HomeViewModel
    {
        public ICommand DistanceTapped => new Command(DisTapped);
        public void DisTapped()
        {
          //  .Show();

        }



        public int DidForEco
        {
            get
            {
                return 75;
            }
            
        }
    }
}

