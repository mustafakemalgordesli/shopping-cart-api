using System.Text.Json.Serialization;

namespace WebAPI.DTOs;

public class ResponseDTO
{
    public ResponseDTO(bool Success)
    {
        this.Success = Success;
        Message = null;
    }

    public ResponseDTO(bool Success, string Message)
    {
        this.Success = Success;
        this.Message = Message;
    }

    public bool Success { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Message { get; set; }
}
