﻿using Microsoft.AspNetCore.Mvc;
using Project1Phase1.Data;
using Project1Phase1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.Repositories
{
    public class HouseholdRepo
    {
        ApplicationDbContext _context;

        public HouseholdRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        public bool CreateHousehold(HomeVM home)
        {
            var household = GetHouseholdByName(home.homeName);
            if (household != null)
            {
                return false;
            }

            var g = Guid.NewGuid().ToString();
            _context.Homes.Add(new Home
            {
                HomeId = g,
                HomeName = home.homeName
            });
            _context.SaveChanges();
            return true;
        }

        public bool AddRoommateToHome(string roommateId, string homeId)
        {
            var household = GetHouseholdById(homeId);
            RoomieRepo roomieRepo = new RoomieRepo(_context);
            var roommate = roomieRepo.GetRoommate(roommateId);
            if (household != null)
            {
                roommate.HomeId = homeId;
                //household.Roommates.Add(roommate);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Home GetHouseholdById(string Id)
        {
            var household = _context.Homes.Where(h => h.HomeId == Id).FirstOrDefault();
            if (household != null)
            {
                return household;
            }
            return null;
        }

        public Home GetHouseholdByName(string name)
        {
            var household = _context.Homes.Where(h => h.HomeName == name).FirstOrDefault();
            if (household != null)
            {
                return household;
            }
            return null;
        }
    }
}
