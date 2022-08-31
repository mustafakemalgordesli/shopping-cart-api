using System.Text.Json;

namespace WebAPI.DTOs
{
    public class ErrorDetailDTO
    {
        public bool Success { get; set; } = false;
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
