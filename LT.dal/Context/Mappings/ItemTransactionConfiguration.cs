using LT.model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.dal.Context.Mappings
{
    public class ItemTransactionConfiguration : EntityBaseConfiguration<EntityItemTransaction>
    {
        public override void ConfigureEntity(EntityTypeBuilder<EntityItemTransaction> builder)
        {

        }
    }
}
