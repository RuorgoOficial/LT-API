using LT.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Context
{
    public class LTDBContext : DbContext
    {
        public DbSet<EntityTest> Test { get; set; }
        public DbSet<EntityScore> Score { get; set; }

        public LTDBContext(DbContextOptions<LTDBContext> options) : base(options) { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityTest>()
                .Property(b => b.CreatedTimestamp)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<EntityTest>()
                .Property(b => b.UpdatedTimestamp)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<EntityScore>()
                .Property(b => b.CreatedTimestamp)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<EntityScore>()
                .Property(b => b.UpdatedTimestamp)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
