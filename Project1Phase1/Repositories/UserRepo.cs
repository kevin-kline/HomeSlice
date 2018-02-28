using Project1Phase1.Data;
using Project1Phase1.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1Phase1.Repositories
{
    public class UserRepo
    {
        ApplicationDbContext _context;

        public UserRepo(ApplicationDbContext context)
        {
            this._context = context;
        }

        // Get all users in the databaFse. 
        public IEnumerable<UserVM> All()
        {
            var users = _context.Users.Select(u => new UserVM()
            {
                Email = u.Email
            });
            return users;
        }

        public string FindUserId(string UserEmail)
        {
            var user = _context.Users.Where(u => u.Email == UserEmail).FirstOrDefault();
            if (user != null)
            {
                return user.Id;
            }
            return null;
        }
    }
}
