namespace BulkyBookWeb.Model
{
    public class Category
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }

        public int DisplayedOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
