using Project1Phase1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.ViewModels
{
    public class AddTransactionVM
    {
        public Roommate currentUser { get; set; }
        public IEnumerable<Roommate> otherRoommates { get; set; }
    }
}
