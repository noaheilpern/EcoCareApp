using System;
using System.Collections.Generic;
using System.Text;

namespace EcoCareApp.Models
{
    class RegularUser
    {
        public RegularUser()
        {
            Goals = new List<int>();
            Sales = new List<Sale>();
            UsersData = new List<UserData>();
        }

        public string UserName { get; set; }
        public DateTime Birthday { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int InitialMeatsMeals { get; set; }
        public bool VeganRareMeat { get; set; }
        public bool Vegetarian { get; set; }
        public string Transportation { get; set; }
        public double DistanceToWork { get; set; }
        public double LastElectricityBill { get; set; }
        public int PeopleAtTheHousehold { get; set; }

        public virtual List<int> Goals { get; set; }
        public virtual List<Sale> Sales { get; set; }
        public virtual List<UserData> UsersData { get; set; }
    }
}
