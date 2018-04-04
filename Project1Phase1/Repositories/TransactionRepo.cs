using Microsoft.EntityFrameworkCore;
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
            RoomieRepo roomieRepo = new RoomieRepo(_context);
            List<Roommate> roommies = roomieRepo.GetAllOtherRoommates(userId).ToList();

            decimal bal = 0;

            foreach (Roommate roommie in roommies)
            {
                decimal individualBal = GetIndividualRelationshipBalance(userId, roommie.RoommateId);
                bal += individualBal;
            }

            return bal;
        }

        public decimal GetIndividualRelationshipBalance(string userId, string roommateId)
        {
            decimal bal = 0;
            //adding all the transactions from the roommate to the current user to the balance
            IEnumerable<RoommateTransaction> transReceived = GetRelationshipTransactionsOneWay(roommateId, userId);
           
            //adding all the transactions from the current user to the roommate to the balance
            IEnumerable<RoommateTransaction> transactionsSent = GetRelationshipTransactionsOneWay(userId, roommateId);

            foreach (var trans in transReceived)
            {
                decimal transBalance = trans.AmountToReceiver;
                bal -= transBalance; //Kevin, for this particular one we need one of them to be -=
                                     //because we are querying the RoommateTransactions twice,
                                     //not the RoommateTransaction table and the Transaction table.
                                     //Hear me out. I realize I may be crazy, but not in this case
                                     //(I think)
            }
            foreach (var trans in transactionsSent)
            {
                decimal transBalance = trans.AmountToReceiver;
                bal += transBalance;
            }
            return bal;
        }

        public IEnumerable<RoommateTransaction> GetAllRelationshipTransactions(string currentUserId, string roommateId)
        {
            IEnumerable<RoommateTransaction> relationshipTransactions1 = GetRelationshipTransactionsOneWay(currentUserId, roommateId);
            IEnumerable<RoommateTransaction> relationshipTransactions2 = GetRelationshipTransactionsOneWay(roommateId, currentUserId);

            IEnumerable<RoommateTransaction> allRelationshipTransactions = relationshipTransactions1.Concat(relationshipTransactions2);

            return allRelationshipTransactions;
        }

        //only gets half the total roommate transactions between two roommates
        private IEnumerable<RoommateTransaction> GetRelationshipTransactionsOneWay(string senderId, string receiverId)
        {
            IEnumerable<RoommateTransaction> oneWayRoomieTransactions = _context.RoommateTransactions
                .Include("Transaction")
                .Where(i => i.ReceiverId == receiverId)
                .Where(i => i.Transaction.SenderId == senderId);
            return oneWayRoomieTransactions;
        }

        public void CreateTransaction(TransactionVM transVm)
        {
            
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
            //if (transVm.amount_of_users > 1)
            //{
                //get amount to receiver by dividing total-amount by amount-of-users + 1 sender 
                amountToReceiver = transVm.amount_total / (transVm.amount_of_users + 1);
            //}else
            //{
            //amountToReceiver = transVm.amount_total;
            //}
            //if (transVm.type == "Bill")
            //{
            //    amountToReceiver *= -1;
            //}
            foreach (string userId in transVm.receivers)
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
