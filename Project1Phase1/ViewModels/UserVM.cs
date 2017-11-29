using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.ViewModels
{
    public class UserVM
    {
        [DisplayName("User ID")]
        public string user_id { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("First Name")]
        public string fName { get; set; }
        [DisplayName("Last Name")]
        public string lName { get; set; }
        [DisplayName("Balance")]
        public decimal balance { get; set; }
        [DisplayName("Password")]
        public string password { get; set; }
    }
}
