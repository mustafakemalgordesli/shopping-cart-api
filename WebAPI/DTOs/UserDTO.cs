using System.Text.Json.Serialization;

namespace WebAPI.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? UpdatedDate { get; set; }
    public string FirstName { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastName { get; set; }
    public string Email { get; set; }
}
