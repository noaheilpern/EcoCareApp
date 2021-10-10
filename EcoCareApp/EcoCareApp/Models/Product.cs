using System;
using System.Collections.Generic;
using System.Text;

namespace EcoCareApp.Models
{
    class Product
    {
        public Product()
        {
            Sales = new List<Sale>();
        }

        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public bool Active { get; set; }
        public string SellersUsername { get; set; }
        public int ProductId { get; set; }

        public virtual List<Sale> Sales { get; set; }
    }
}
