using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project1Phase1.Models;

namespace Project1Phase1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message Two"] = "Another application description";

            return View();
        }
        public IActionResult JoinCreate()
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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
