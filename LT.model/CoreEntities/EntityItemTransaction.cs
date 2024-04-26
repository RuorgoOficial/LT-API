using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace LT.model
{
    [Table("ItemTransaction")]
    public class EntityItemTransaction : EntityBase
    {
        public int ItemId { get; set; }
        public Guid TransactionGuid { get; set; }
        public int Quantity { get; set; }

    }
}