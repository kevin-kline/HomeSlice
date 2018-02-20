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

        public decimal GetTotalBalance(string userId)
        {
            decimal bal = 0;
            //adding all the payments from the roommates to the current user to the balance
            IEnumerable<RoommateTransaction> roomieTransactions = _context.RoommateTransactions
                .Where(i => i.ReceiverId == userId);
            foreach (var trans in roomieTransactions)
            {
                decimal transBalance = trans.AmountToReceiver;
                bal += transBalance;
            }
            //adding all the bill amounts that the current user has posted to the balance
            IEnumerable<Transaction> transactions = _context.Transactions
                .Where(t => t.SenderId == userId);
            foreach (var transaction in transactions)
            {
                IEnumerable<RoommateTransaction> bills = transaction.RoommateTransactions;
                foreach (var trans in bills)
                {
                    decimal billBalance = trans.AmountToReceiver;
                    bal += billBalance;
                }
            }
            return bal;
        }

    }
}
