using Project1Phase1.Data;
using Project1Phase1.ViewModels;
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
            IEnumerable<RoommateTransaction> transReceived = _context.RoommateTransactions
                .Where(i => i.ReceiverId == userId);
            foreach (var trans in transReceived)
            {
                bal += trans.AmountToReceiver;
            }
            //adding all the bill amounts that the current user has posted to the balance
            IEnumerable<Transaction> transactions = _context.Transactions
                .Where(t => t.SenderId == userId);
            foreach (var transaction in transactions)
            {
                IEnumerable<RoommateTransaction> transSent = _context.RoommateTransactions
                    .Where(i => i.TransactionId == transaction.TransactionId);
                foreach (var trans in transSent)
                {
                    bal += trans.AmountToReceiver;
                }
            }
            Console.WriteLine(bal);
            return bal;
        }

        public decimal GetIndividualRelationshipBalance(string userId, string roommateId)
        {
            decimal bal = 0;
            //adding all the transactions from the roommate to the current user to the balance
            IEnumerable<RoommateTransaction> transReceived = _context.RoommateTransactions
                .Where(i => i.ReceiverId == userId)
                .Where(i => i.Transaction.SenderId == roommateId);
            foreach (var trans in transReceived)
            {
                decimal transBalance = trans.AmountToReceiver;
                bal += transBalance; //Kevin, for this particular one we need one of them to be -=
                                     //because we are querying the RoommateTransactions twice,
                                     //not the RoommateTransaction table and the Transaction table.
                                     //Hear me out. I realize I may be crazy, but not in this case
                                     //(I think).
            }
            //adding all the transactions from the current user to the roommate to the balance
            IEnumerable<RoommateTransaction> transactionsSent = _context.RoommateTransactions
                .Where(i => i.ReceiverId == roommateId)
                .Where(i => i.Transaction.SenderId == userId);
            foreach (var trans in transactionsSent)
            {
                decimal transBalance = trans.AmountToReceiver;
                bal -= transBalance;
            }
            return bal;
        }

        public void CreateTransaction(TransactionVM transVm)
        {
            if(transVm.type == "Bill")
            {
                transVm.amount_total *= -1;
            }
            var transaction = new Transaction
                {
                    SenderId = transVm.sender_id,
                    Name = transVm.name,
                    Type = transVm.type,
                    DateTime = transVm.date,
                    AmountOfRoommates = transVm.amount_of_users,
                    AmountTotal = transVm.amount_total
                };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            decimal amountToReceiver;
            if (transVm.amount_of_users > 1)
            {
                //get amount to receiver by dividing total-amount by amount-of-users + 1 sender 
                amountToReceiver = transVm.amount_total / (transVm.amount_of_users + 1);
            }else
            {
                amountToReceiver = transVm.amount_total;
            }
            foreach (string userId in transVm.recievers)
            {
                CreateRoommateTransaction(transaction.TransactionId, 
                    userId, amountToReceiver);
            }
        }

        public void CreateRoommateTransaction(int transactionId, string receiverId, decimal amountToReceiver)
        {
            _context.RoommateTransactions.Add(new RoommateTransaction
            { 
                TransactionId = transactionId,
                ReceiverId = receiverId,
                AmountToReceiver = amountToReceiver
            });
            _context.SaveChanges();
        }

    }
}
