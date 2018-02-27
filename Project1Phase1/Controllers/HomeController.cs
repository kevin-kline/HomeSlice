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
        public IActionResult JoinCreateHousehold()
        {
            return View();
        }
         [Authorize(Roles = "HouseholdAdmin")] 
        public IActionResult ManageHousehold()
        {
            return View();
        }
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
            //get all other balances with roomies 

            //create VM for profile with all the above info

            return View(/* insert VM here */);
        }
        public IActionResult Relationship()
        {
            return View();
        }
        public IActionResult AddBill()
        {
            return View();
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
