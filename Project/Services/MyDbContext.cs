
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Services
{
    public class MyDbContext : IdentityDbContext
    {
        public DbSet<Profile> TblProfile { get; set; }
        public DbSet<Category> TblCategory { get; set; }
        public DbSet<Hamper> TblHamper { get; set; }
        public DbSet<Address> TblAddress { get; set; }
        public DbSet<Order> TblOrder { get; set; }
        public DbSet<OrderLineItem> TblOrderLineItem { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=ProjectDatabase ;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Composite Primary key
            builder.Entity<OrderLineItem>()
                    .HasKey(t => new { t.OrderId, t.HamperId });

            //now creating many to many relationship 
            builder.Entity<OrderLineItem>()
                    .HasOne(p => p.Order)
                    .WithMany(x => x.Hampers)
                    .HasForeignKey(y => y.OrderId);

            builder.Entity<OrderLineItem>()
                    .HasOne(s => s.Hamper)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(z => z.HamperId);
        }

    }
}
