using System.ComponentModel.DataAnnotations.Schema;

namespace LT.model
{
    public class EntityBase
    {
        public EntityBase() { 
        }
        public int Id {  get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedTimestamp { 
            get {
                return this.createdTimestamp.HasValue
                   ? this.createdTimestamp.Value
                   : DateTime.Now;
            }
            set { this.createdTimestamp = value; }
        }
        private DateTime? createdTimestamp = null;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedTimestamp { 
            get {
                return this.updatedTimestamp.HasValue
                   ? this.updatedTimestamp.Value
                   : DateTime.Now;
            }
            set { this.updatedTimestamp = value; }
        }
        private DateTime? updatedTimestamp = null;
    }
}