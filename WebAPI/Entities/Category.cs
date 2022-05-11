using System.Text.Json.Serialization;

namespace WebAPI.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
