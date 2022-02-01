using System;
using System.Collections.Generic;
using System.Text;

namespace EcoCareApp.Models
{
    public class Seller
    {
        public string UserName { get; set; }
        public string PhoneNum { get; set; }


        public virtual User UserNameNavigation { get; set; }

    }

}
