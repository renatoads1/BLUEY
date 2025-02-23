﻿using BLUEY.Models;
using BLUEY.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BLUEY.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LCTOFISConsServ> lCTOFISConsServs {  get; set; }
        public DbSet<IdentityRole> roles { get; set; }
        public DbSet<IdentityUserRole<string>> userRoles { get; set; }
        //public DbSet<Users> users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define varchar(255) como tipo padrão para strings
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    if (property.ClrType == typeof(string) && property.GetColumnType() == null)
                    {
                        property.SetColumnType("varchar(255)");
                    }
                }
            }
        }
    }
}
