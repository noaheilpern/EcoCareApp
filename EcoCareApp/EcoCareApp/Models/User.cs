using System;
using System.Collections.Generic;


namespace EcoCareApp.Models
{
    public partial class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Country { get; set; }

        public bool IsAdmin { get; set; }

        //without is admin because it can not be changed
        public bool Equals(User u)
        {
            return this.UserName == u.UserName && this.Email == u.Email && this.Pass == u.Pass
                && this.FirstName == u.FirstName && this.LastName == u.LastName &&
                this.Country == u.Country;
        }
    }
}

