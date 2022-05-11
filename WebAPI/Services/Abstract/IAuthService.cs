using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Services.Abstract;

public interface IAuthService
{
    Task<AuthDTO> Register(RegisterDTO request);
    Task<AuthDTO> Login(LoginDTO request);
}
