namespace LT.model
{
    public class EntityBaseDto
    {
        public EntityBaseDto() { 
        }
        public int Id {  get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
    }
}