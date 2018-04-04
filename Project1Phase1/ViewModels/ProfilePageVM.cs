using Project1Phase1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.ViewModels
{
    public class RoomieAndBalance
    {
        public Roommate Roommate { get; set; }
        public decimal Balance { get; set; }
    }
    public class ProfilePageVM
    {
        public RoomieAndBalance CurrentUser { get; set; }
        public List<RoomieAndBalance> RoomiesRelationships { get; set; }
        [DisplayName("Home ID")]
        public string homeId { get; set; }
        [DisplayName("Home Name")]
        public string homeName { get; set; }
    }
}
