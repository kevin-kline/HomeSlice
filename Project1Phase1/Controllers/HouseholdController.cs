using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project1Phase1.Services;
using Project1Phase1.Data;
using Project1Phase1.Repositories;
using Project1Phase1.ViewModels;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using Microsoft.AspNetCore.Authorization;

namespace Project1Phase1.Controllers
{
    public class HouseholdController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private IServiceProvider _serviceProvider;

        public HouseholdController(ApplicationDbContext context, IEmailSender emailSender, IServiceProvider serviceProvider)
        {
            _emailSender = emailSender;
            _context = context;
            _serviceProvider = serviceProvider;
        }

        [Authorize]
        [HttpGet]
        public IActionResult JoinCreateHousehold(string errorMessage)
        {
            if (errorMessage != null)
            {
                ViewBag.Error = errorMessage;
            }
            return View();
        }

        [ValidateRecaptcha]
        [HttpPost]
        public IActionResult Create(HomeVM home)
        {
            HouseholdRepo householdRepo = new HouseholdRepo(_context);
            if (householdRepo.CreateHousehold(home))
            {
                var _home = householdRepo.GetHouseholdByName(home.homeName);

                //Role Assignment
                UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider);
                userRoleRepo.AddUserRole(User.Identity.Name, "HomeAdmin");

                return RedirectToAction("Join", _home);
            }
            
            return RedirectToAction(nameof(JoinCreateHousehold), new { errorMessage = "Failed to create the household. Try a new name, please."});
        }

        [ValidateRecaptcha]
        [HttpPost]
        public IActionResult Join(HomeVM home)
        {
            HouseholdRepo householdRepo = new HouseholdRepo(_context);
            var _home = householdRepo.GetHouseholdByName(home.homeName);
            if (_home != null)
            {
                if (_home.HomeId == home.homeId)
                {
                    UserRepo userRepo = new UserRepo(_context);
                    var currentUserEmail = User.Identity.Name;
                    var userId = userRepo.FindUserId(currentUserEmail);
                    householdRepo.AddRoommateToHome(userId, _home.HomeId);

                    //Role Assignment
                    UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider);
                    userRoleRepo.AddUserRole(currentUserEmail, "Roommate");

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            
            return RedirectToAction(nameof(JoinCreateHousehold), new { errorMessage = "Failed to join the household. Make sure the Home Name and Household Password are correct, please." });
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