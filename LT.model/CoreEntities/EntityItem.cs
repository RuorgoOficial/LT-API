using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace LT.model
{
    [Table("Item")]
    [Index(nameof(Name), IsUnique = true)]
    public class EntityItem : EntityBase
    {
        [StringLength(450)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<EntityItemTransaction> Transactions { get; set; } = Array.Empty<EntityItemTransaction>();

    }
}