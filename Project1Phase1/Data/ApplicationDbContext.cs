using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project1Phase1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project1Phase1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public class User
        {
            [Key]
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public string HomeId { get; set; }
            
            
            //Navigation Properties
            public virtual Home Home { get; set; }
            public virtual Transaction Transaction { get; set; }
            
        }
        public class Home
        {
            [Key]

            public int HomeId { get; set; }
            public string HomeName { get; set; }

            //Navigation Properties
            public virtual User User { get; set; }
        }
        public class Transaction
        {
            [Key]
            public int TransactionId { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public DateTime DateTime { get; set; }
            public int AmountOfUsers { get; set; }
          
            public int SenderId { get; set; }

            //navigation Properties
            public virtual User User { get; set; }
            public virtual UserTransaction UserTransaction { get; set; }
        }
        public class UserTransaction
        {
            [Key, Column(Order = 0)]
            
            public int TransactionId { get; set;}
            [Key, Column(Order = 1)]
           
            public int ReceiverId { get; set; }
            public decimal AmountToReceiver { get; set; }

            //Navigation Properties
            public virtual User User { get; set; }
            public virtual Transaction Transaction { get; set; }
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        //define entity collections

            public DbSet<User>  Users { get; set; }
            public DbSet<Transaction> Transactions { get; set; }
            public DbSet<UserTransaction> UserTransactions { get; set; }
            public DbSet<Home> Homes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>();





        }
    }
}
