using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using net_design_pattern.Domain.Models;

namespace net_design_pattern.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<Account> Accounts {get; set;}
        public DbSet<Profile> Profiles {get; set;}
        public DbSet<Role> Roles {get; set;}

        public DbSet<AccountHasRole> AccountHasRoles {get; set;}
        public DbSet<Product> Products {get; set;}
        public DbSet<Category> Categories {get; set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Account>()
            .HasMany(x => x.AccountHasRoles)
            .WithOne(x => x.Account);
            
            builder.Entity<Account>()
            .HasOne(x => x.Profile)
            .WithOne(x => x.Account);

            builder.Entity<Role>()
            .HasMany(x => x.AccountHasRoles)
            .WithOne(x => x.Role);

            builder.Entity<Product>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Products);
        }
    }
}