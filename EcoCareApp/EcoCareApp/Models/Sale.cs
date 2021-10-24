using System;
using System.Collections.Generic;
using System.Text;

namespace EcoCareApp.Models
{
    public class Sale
    {
        public string BuyerUserName { get; set; }
        public int ProductId { get; set; }
        public int DateBought { get; set; }
        public int PriceBought { get; set; }
        public int SaleId { get; set; }

        public virtual RegularUser BuyerUserNameNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
