using System.ComponentModel.DataAnnotations.Schema;

namespace LT.model
{
    [Table("Test")]
    public class EntityTest : EntityBase
    {
        public string Description { get; set; } = string.Empty;

    }
}