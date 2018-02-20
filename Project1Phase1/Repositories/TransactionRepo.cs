using Project1Phase1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.Repositories
{
    public class TransactionRepo
    {

        ApplicationDbContext _context;

        public TransactionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal GetTotalBalance()
        {

            decimal bal = 0;
            return bal;
        }

    }
}
