namespace ActivitySimulator.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ActivitySimulator.Data.Models.Olx;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Image> Images { get; set; }
    }
}
