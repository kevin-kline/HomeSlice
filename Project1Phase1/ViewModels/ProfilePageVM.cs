using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.ViewModels
{
    public class RoomieAndBalance
    {
        public string RoommateName { get; set; }
        public decimal Balance { get; set; }
    }
    public class ProfilePageVM
    {
        public RoomieAndBalance CurrentUser { get; set; }
        public List<RoomieAndBalance> RoomiesRelationships { get; set; }
    }
}
