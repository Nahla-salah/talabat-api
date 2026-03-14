using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Identity
{
    public class CoreveraIdentityDbContext:IdentityDbContext
    {
       
        public CoreveraIdentityDbContext(DbContextOptions<CoreveraIdentityDbContext> options) : base(options)
        {

        }
        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses");
            builder.Entity<ApplicationUser>()
        .HasOne(u => u.Address)          
        .WithOne(a => a.ApplicationUser) 
        .HasForeignKey<Address>(a => a.UserId);

        }


      



    }
}
