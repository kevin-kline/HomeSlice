using Project1Phase1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.Repositories
{
    public class RoomieRepo
    {
        private ApplicationDbContext _context;

        public RoomieRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public void AddRoommate(string appUserId, string fName, string lName)
        {
            Roommate r1 = new Roommate { RoommateId = appUserId, FirstName = fName, LastName = lName };
            _context.Roommates.Add(r1);
            _context.SaveChanges();
        }
    }
}
