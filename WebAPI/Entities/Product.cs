namespace WebAPI.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int Quantity { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public virtual Category Category { get; set; }
    }
}
