using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project1Phase1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Project1Phase1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
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
            return View();
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
