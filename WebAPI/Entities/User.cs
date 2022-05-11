using System.Text.Json.Serialization;

namespace WebAPI.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LastName { get; set; }
    public string Email { get; set; }
    //[JsonIgnore]
    public byte[] PasswordHash { get; set; }
    //[JsonIgnore]
    public byte[] PasswordSalt { get; set; }
    //[JsonIgnore]
    public bool Status { get; set; } = true;
    public string Role { get; set; } = "User";
}
