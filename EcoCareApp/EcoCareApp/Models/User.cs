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
        public bool IsAdmin { get; set; }
    }
}

