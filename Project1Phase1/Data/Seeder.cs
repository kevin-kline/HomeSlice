using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.Data
{
    public class Seeder
    {
        private ApplicationDbContext _context;

        public Seeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            //Exit if data exists.
            if (_context.Roommates.Count() != 0)
            {
                return;
            }
            //Add a home
            //Home h1 = new Home { HomeName = "Innocent Angels", Roommates = null };
            Home h1 = new Home { HomeName = "Innocent Angels" };

            //Add home to db 
            _context.Homes.Add(h1);
            _context.SaveChanges();

            //Add Roommates
            Roommate r1 = new Roommate { RoommateId = "1", FirstName = "Kevin", HomeId = h1.HomeId, LastName = "Kline"  };
            _context.Roommates.Add(r1);
            Roommate r2 = new Roommate { RoommateId = "2", FirstName = "Tom", HomeId = h1.HomeId, LastName = "Setterlund" };
            _context.Roommates.Add(r2);
            Roommate r3 = new Roommate { RoommateId = "3", FirstName = "Michael", HomeId = h1.HomeId, LastName = "Van Zande" };
            _context.Roommates.Add(r3);

            _context.SaveChanges();

            //Add transaction
            Transaction t1 = new Transaction
            {
                AmountOfRoommates = 3,
                DateTime = DateTime.Now,
                Name = "Rent",
                Type = "Bill",
                SenderId = r1.RoommateId
            };
            _context.Transactions.Add(t1);
            _context.SaveChanges();

            //Add Roommate transaction
            RoommateTransaction rt1 = new RoommateTransaction { ReceiverId = r2.RoommateId, AmountToReceiver = 100 };
            RoommateTransaction rt2 = new RoommateTransaction { ReceiverId = r3.RoommateId, AmountToReceiver = 100 };
            _context.RoommateTransactions.Add(rt1);
            _context.RoommateTransactions.Add(rt2);

            _context.SaveChanges();

        }
    }
}
