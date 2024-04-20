using LT.model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace LT.dal.Context.Mappings
{
    public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : EntityBase
    {
        public void Configure(EntityTypeBuilder<TEntity> modelBuilder)
        {
            modelBuilder.Property(b => b.CreatedTimestamp)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Property(b => b.UpdatedTimestamp)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Property(b => b.CreatedTimestamp)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Property(b => b.UpdatedTimestamp)
                .HasDefaultValueSql("GETDATE()");

            ConfigureEntity(modelBuilder);
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}
