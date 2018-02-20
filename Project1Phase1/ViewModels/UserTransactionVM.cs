using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.ViewModels
{
    public class UserTransactionVM
    {
        public string transaction_id { get; set; }
        public string receiver_id { get; set; }
        public decimal amount_to_receiver { get; set; }
    }
}
