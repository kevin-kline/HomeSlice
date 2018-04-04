using Project1Phase1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.ViewModels
{
    public class TransactionVM
    {
        [DisplayName("Name")]
        public string name { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
        [RegularExpression("[1-9][0-9]*")]
        public decimal amount_total { get; set; }
        public int amount_of_users { get; set; }
        public string sender_id { get; set; }
        public List<string> receivers { get; set; }
    }
}
