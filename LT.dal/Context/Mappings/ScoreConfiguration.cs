using LT.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Context.Mappings
{
    public class ScoreConfiguration : EntityBaseConfiguration<EntityScore>
    {
        public override void ConfigureEntity(EntityTypeBuilder<EntityScore> builder)
        {

        }
    }
}
