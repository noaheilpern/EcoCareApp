using System;
using System.Collections.Generic;
using System.Text;

namespace EcoCareApp.Models
{
    class UserData
    {
        public double DistanceToWork { get; set; }
        public double ElecticityUsagePerWeek { get; set; }
        public int MeatsMeals { get; set; }
        public int DateT { get; set; }
        public string UserName { get; set; }

        public virtual RegularUser UserNameNavigation { get; set; }
    }
}
