using System;
using System.Collections.Generic;
using System.Text;
using EcoCareApp.Models;
using EcoCareApp.Services;

namespace EcoCareApp.ViewModels
{
    class ProductsViewModel
    {
        public ProductsViewModel()
        {
           
        }
        
        public List<Product> ProductsList
        {
            get
            {
                EcoCareAPIProxy proxy = EcoCareAPIProxy.CreateProxy();
                //return await proxy.GetProductsAsync();
                return null;
            }
        }

    }
}
