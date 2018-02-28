using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project1Phase1.Services;
using Project1Phase1.Data;
using Project1Phase1.Repositories;
using Project1Phase1.ViewModels;

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

        public IActionResult Create(HomeVM home)
        {
            HouseholdRepo householdRepo = new HouseholdRepo(_context);
            if (householdRepo.CreateHousehold(home))
            {
                return RedirectToAction("Join", home);
            }
            return RedirectToAction(nameof(HomeController.JoinCreateHousehold), "JoinCreateHousehold");
        }

        public IActionResult Join(HomeVM home)
        {
            HouseholdRepo householdRepo = new HouseholdRepo(_context);
            var _home = householdRepo.GetHouseholdByName(home.homeName);
            if (_home.HomeId == home.homeId)
            {
                UserRepo userRepo = new UserRepo(_context);
                var currentUserEmail = User.Identity.Name;
                var userId = userRepo.FindUserId(currentUserEmail);
                householdRepo.AddRoommateToHome(userId, _home.HomeId);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return RedirectToAction(nameof(HomeController.JoinCreateHousehold), "JoinCreateHousehold");
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