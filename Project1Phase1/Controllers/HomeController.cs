using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project1Phase1.Models;
using Microsoft.AspNetCore.Authorization;
using Project1Phase1.Repositories;
using Project1Phase1.Services;
using Project1Phase1.Data;
using Project1Phase1.ViewModels;

namespace Project1Phase1.Controllers
{
    public class HomeController : Controller
    {

        ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
                
        [Authorize(Roles = "HomeAdmin")] 
        public IActionResult ManageHousehold()
        {
            return View();
        }
        [Authorize]
        public IActionResult Profile()
        {
            //get current users total balance
            string userId = User.getUserId();
            TransactionRepo TransRepo = new TransactionRepo(_context);
            decimal totalBalance = TransRepo.GetTotalBalance(userId);
            //get all other roommates
            RoomieRepo roomieRepo = new RoomieRepo(_context);
            IEnumerable<Roommate> roommates = roomieRepo
                .GetAllOtherRoommates(userId);
            //create and fill VMs
                //get current user name
            Roommate currentSignedInUser = roomieRepo.GetRoommate(userId);
            RoomieAndBalance currentUser = new RoomieAndBalance()
            {
                Balance = totalBalance,
                Roommate = currentSignedInUser
            };
            ProfilePageVM ppvm = new ProfilePageVM()
            {
                CurrentUser = currentUser,
                RoomiesRelationships = new List<RoomieAndBalance>(),
                
            };
            //get all other balances with roomies, put them into a VM,
            //which then goes into another bigger VM
            if (roommates != null) {
                foreach (var roomie in roommates)
                {
                    decimal relationshipBalance =
                        TransRepo.GetIndividualRelationshipBalance(userId, roomie.RoommateId);
                    RoomieAndBalance roomieAndBalance = new RoomieAndBalance()
                    {
                        Roommate = roomie,
                        Balance = relationshipBalance
                    };
                    ppvm.RoomiesRelationships.Add(roomieAndBalance);
                }
            }
            return View(ppvm);
        }
        public IActionResult Relationship()
        {


            return View();
        }
        public IActionResult AddBill()
        {
            // For passing simple types.
            //   return RedirectToAction("ManageBills", new { productID = 5, productName = "bob"});
            string userId = User.getUserId();
            RoomieRepo roomieRepo = new RoomieRepo(_context);
            Roommate currentSignedInUser = roomieRepo.GetRoommate(userId);
            IEnumerable<Roommate> roommates = roomieRepo.GetAllOtherRoommates(userId);
            AddTransactionVM addTransactionVM = new AddTransactionVM { currentUser = currentSignedInUser, otherRoommates = roommates };
            return View(addTransactionVM);
        }
        
        [HttpPost]
        public IActionResult HandleTransaction(TransactionVM transVM)
        {
            TransactionRepo transRepo = new TransactionRepo(_context);
            transVM.amount_of_users = transVM.recievers.Length;
            transRepo.CreateTransaction(transVM);
            return RedirectToAction("Profile");
        }
        public IActionResult ManageBills()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
