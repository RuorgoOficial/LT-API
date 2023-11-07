using LT.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Context
{
    public class LTContext : DbContext
    {
        public DbSet<EntityTest> Test { get; set; }
        public DbSet<EntityScore> Score { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(LocalDb)\MSSQLLocalDB;Database=LTDb;Trusted_Connection=True");

            //optionsBuilder.UseSqlLite(
            //    @"Server=(LocalDb)\MSSQLLocalDB;Database=LTDb;Trusted_Connection=True");
        }
    }
}
