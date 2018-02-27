using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project1Phase1.Services;
using Project1Phase1.Data;
using Project1Phase1.Repositories;

namespace Project1Phase1.Controllers
{
    public class HouseholdController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public HouseholdController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string name, string address)
        {
            HouseholdRepo householdRepo = new HouseholdRepo(_context);
            if (householdRepo.CreateHousehold(name))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return RedirectToAction(nameof(HomeController.Index), "JoinCreateHousehold");
        }

        //public IActionResult Invite(string householdName, string email)
        //{
        //    HouseholdRepo householdRepo = new HouseholdRepo(_context);
        //    var household = householdRepo.GetHouseholdByName(householdName);
        //    var id = household.HomeId.ToString();
        //    var code = "password";
        //    var callbackUrl = Url.EmailConfirmationLink(id , code, Request.Scheme);
        //    _emailSender.SendEmailConfirmationAsync(email, callbackUrl);

        //    return View();
        //}
    }
}