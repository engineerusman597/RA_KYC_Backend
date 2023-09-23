﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RA_KYC_BE.Domain.Entities;
using RA_KYC_BE.Infrastructure.Identity.Models;
using RA_KYC_BE.Domain.Entities;

namespace Infrastructure.Content.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BusinessTypes> BusinessTypes { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<CustomerRiskFactors> CustomerRiskFactors { get; set; }
        public DbSet<CustomerTypes> CustomerTypes { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<RiskCategories> RiskCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "Identity.User");
            });

            builder.Entity<AppRole>(entity =>
            {
                entity.ToTable(name: "Identity.Role");
            });
            builder.Entity<AppUserRole>(entity =>
            {
                entity.ToTable("Identity.UserRoles");
            });

            builder.Entity<AppUserClaim>(entity =>
            {
                entity.ToTable("Identity.UserClaims");
            });

            builder.Entity<AppUserLogin>(entity =>
            {
                entity.ToTable("Identity.UserLogins");
            });

            builder.Entity<AppRoleClaim>(entity =>
            {
                entity.ToTable("Identity.RoleClaims");

            });

            builder.Entity<AppUserToken>(entity =>
            {
                entity.ToTable("Identity.UserTokens");
            });
        }
    }
}