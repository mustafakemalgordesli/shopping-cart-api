using System.Text.Json.Serialization;

namespace WebAPI.DTOs;

public class ResponseDataDTO<T> : ResponseDTO
{
    public ResponseDataDTO(bool Success) : base(Success)
    {
    }

    public ResponseDataDTO(bool Success, string Message) : base(Success, Message)
    {
    }
    public ResponseDataDTO(bool Success, string Message, T Data) : base(Success, Message)
    {
        this.Data = Data;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }
}
