using System;
using System.Collections.Generic;
using System.Text;

namespace EcoCareApp.Models
{
    public class Seller
    {
        public string UserName { get; set; }
        public string PhoneNum { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }


        public virtual User UserNameNavigation { get; set; }

        public bool Equals(Seller s)
        {
            return this.PhoneNum == s.PhoneNum && this.UserName == s.UserName;
        }
    }

}
