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
    public class Roommate
    {
        [Key]
        public string RoommateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomeId { get; set; }


        //Navigation Properties
        public virtual Home Home { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<RoommateTransaction> RoommateTransactions { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
    public class Home
    {
        [Key]
        public string HomeId { get; set; }
        public string HomeName { get; set; }

        //Navigation Properties
        public virtual ICollection<Roommate> Roommates { get; set; }
    }
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; }
        public int AmountOfRoommates { get; set; }
        public decimal AmountTotal { get; set; }
        public string SenderId { get; set; }

        //navigation Properties
        public virtual Roommate Roommate { get; set; }
        public virtual ICollection<RoommateTransaction> RoommateTransactions { get; set; }
    }
    public class RoommateTransaction
    {
        [Key, Column(Order = 0)]

        public int TransactionId { get; set; }
        [Key, Column(Order = 1)]

        public string ReceiverId { get; set; }
        public decimal AmountToReceiver { get; set; }

        //Navigation Properties
        public virtual Roommate Roommate { get; set; }
        public virtual Transaction Transaction { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }
        //define entity collections

        public DbSet<Roommate> Roommates { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RoommateTransaction> RoommateTransactions { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.Entity<RoommateTransaction>()
                .HasKey(ut => new { ut.TransactionId, ut.ReceiverId });

            builder.Entity<Roommate>()
                .HasOne(r => r.Home)
                .WithMany(r => r.Roommates)
                .HasForeignKey(fk => new { fk.HomeId })
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Roommate>()
            //    .WithMany(r => r.Roommates)
            //    .HasForeignKey(fk => new { fk.HomeId })
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transaction>()
                .HasOne(t => t.Roommate)
                .WithMany(t => t.Transactions)
                .HasForeignKey(fk => new { fk.SenderId })
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RoommateTransaction>()
                .HasOne(rt => rt.Roommate)
                .WithMany(rt => rt.RoommateTransactions)
                .HasForeignKey(fk => new { fk.ReceiverId })
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RoommateTransaction>()
                .HasOne(rt => rt.Transaction)
                .WithMany(rt => rt.RoommateTransactions)
                .HasForeignKey(fk => new { fk.TransactionId })
                 .OnDelete(DeleteBehavior.Restrict);

            //link roommate table to identity table

            builder.Entity<ApplicationUser>()
                .HasOne(r => r.Roommate)
                .WithOne(au => au.ApplicationUser)
                .HasForeignKey<Roommate>(r => r.RoommateId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

        }
    }
}
