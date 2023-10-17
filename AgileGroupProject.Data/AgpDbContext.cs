using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileGroupProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgileGroupProject.Data
{
    public class AgpDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        public AgpDbContext(DbContextOptions<AgpDbContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().ToTable("Users");
        }
    }
}