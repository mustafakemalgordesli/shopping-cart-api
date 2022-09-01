using WebAPI.Entities;

namespace WebAPI.Utils
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public Tuple<int, string>? ValidateToken(string token);
    }
}
