using System;
using System.Collections.Generic;
using System.Text;

namespace EcoCareApp.Models
{
    public class UserData
    {
        public int CategoryId { get; set; }
        public double CategoryValue { get; set; }
        public DateTime DateT { get; set; }
      
        public string UserName { get; set; }

        public virtual RegularUser UserNameNavigation { get; set; }
    }
}
