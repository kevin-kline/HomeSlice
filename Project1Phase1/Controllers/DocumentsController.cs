using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Project1Phase1.Controllers
{
    public class DocumentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ERD()
        {
            return View();
        }
        public IActionResult UseCase()
        {
            return View();
        }
        public IActionResult ApiDoc()
        {
            return View();
        }
        public IActionResult FunctionalRequirements()
        {
            return View();
        }
    }
}