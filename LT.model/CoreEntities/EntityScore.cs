using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace LT.model
{
    [Table("Score")]
    public class EntityScore : EntityBase
    {
        [Precision(10, 2)]
        public decimal Score { get; set; }
        [StringLength(3)]
        public string Acronym { get; set; } = string.Empty;

    }
}