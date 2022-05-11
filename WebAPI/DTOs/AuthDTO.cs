using System.Text.Json.Serialization;

namespace WebAPI.DTOs
{
    public class AuthDTO : ResponseDataDTO<UserDTO>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Token { get; set; }

        //public AuthDTO(bool Success, string Message, string Token) : base(Success, Message)
        //{
        //    this.Token = Token;
        //}


        public AuthDTO(bool Success, string Message, string Token, UserDTO Data) : base(Success, Message, Data)
        {
            this.Token = Token;
        }

        public AuthDTO(bool Success) : base(Success)
        {
        }

        public AuthDTO(bool Success, string Message) : base(Success, Message)
        {
        }
    }
}
