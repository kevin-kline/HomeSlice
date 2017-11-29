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
        public string user_id;
        [DisplayName("First Name")]
        public string fName;
        [DisplayName("Last Name")]
        public string lName;
        [DisplayName("Balance")]
        public decimal balance;
    }
}
