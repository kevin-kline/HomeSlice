using Project1Phase1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.ViewModels
{
    public class RelationshipVM
    {
        public Roommate CurrentUser { get; set; }
        public Roommate Roommate { get; set; }
        public decimal RelationshipBalance { get; set; }
        public IEnumerable<RoommateTransaction> OneRoommateTranstactions { get; set; }
    }
}