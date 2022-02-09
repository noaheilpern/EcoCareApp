using System;
using System.Collections.Generic;
using System.Text;

namespace EcoCareApp.Models
{
    public class RegularUser
    {
        public RegularUser()
        {
            Goals = new List<int>();
            Sales = new List<Sale>();
            UsersData = new List<UserData>();
        }

        public string UserName { get; set; }
        public DateTime Birthday { get; set; }
        public int InitialMeatsMeals { get; set; }
        public bool VeganRareMeat { get; set; }
        public bool Vegetarian { get; set; }
        public string Transportation { get; set; }
        public double DistanceToWork { get; set; }
        public double LastElectricityBill { get; set; }
        public int PeopleAtTheHousehold { get; set; }

        public int Stars { get; set; }

        public virtual List<int> Goals { get; set; }
        public virtual List<Sale> Sales { get; set; }
        public virtual List<UserData> UsersData { get; set; }
        public virtual User UserNameNavigation { get; set; }

        //only to the parameters that can be changed
        public bool Equals(RegularUser r)
        {
            return this.UserName == r.UserName && this.Birthday == r.Birthday && this.PeopleAtTheHousehold == r.PeopleAtTheHousehold;
        }

    }
}
