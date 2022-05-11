using System.Text.Json.Serialization;

namespace WebAPI.DTOs;

public class ProductGetDTO
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UpdatedDate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public int Quantity { get; set; }
    public string ImageURL { get; set; }
    public decimal Price { get; set; }
}
