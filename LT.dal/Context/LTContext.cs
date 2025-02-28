using LT.dal.Context.Mappings;
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
        public DbSet<EntityItem> Item { get; set; }
        public DbSet<EntityItemTransaction> ItemTransaction { get; set; }

        public LTDBContext(DbContextOptions<LTDBContext> options) : base(options) { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ScoreConfiguration())
                .ApplyConfiguration(new TestConfiguration())
                .ApplyConfiguration(new ItemConfiguration())
                .ApplyConfiguration(new ItemTransactionConfiguration());
        }
    }
}
