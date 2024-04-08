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
    public class IdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) :
            base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(entity => { 
                entity.ToTable("User"); 
                entity.HasKey(entity => entity.Id);
            });
            builder.Entity<IdentityRole>(entity => { 
                entity.ToTable("Role"); 
                entity.HasKey(entity => entity.Id);
            });
            builder.Entity<IdentityUserClaim<string>>(entity => { 
                entity.ToTable("UserClaim");
                entity.HasKey(entity => entity.Id);
            });
            builder.Entity<IdentityUserLogin<string>>(entity => { 
                entity.ToTable("UserLogin");
                entity.HasKey(entity => new { entity.UserId, entity.LoginProvider, entity.ProviderKey });
            });
            builder.Entity<IdentityUserRole<string>>(entity => { 
                entity.ToTable("UserRole");
                entity.HasKey(entity => new { entity.UserId, entity.RoleId });
            });

        }
    }
}
