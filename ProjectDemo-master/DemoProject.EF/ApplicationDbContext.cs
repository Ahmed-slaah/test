using DemoProject.Core.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Vacancies> Vacancies { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserRole>().HasKey(r => new { r.UserId, r.RolesId });

            builder.Entity<UserRole>()
                .HasOne<User>(r => r.User)
                .WithMany(m => m.UserRole)
                .HasForeignKey(r => r.UserId);


            builder.Entity<UserRole>()
                .HasOne<Roles>(r => r.Roles)
                .WithMany(u => u.UserRole)
                .HasForeignKey(r => r.RolesId);

            builder.Entity<Categories>()
                   .HasMany(v => v.Vacancies)
                   .WithOne(c => c.Categories);
        }
    }
}