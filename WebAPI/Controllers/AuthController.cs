using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMapper _mapper;
        private IAuthService _authService;
        public AuthController(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO request)
        {
            AuthDTO response = await _authService.Register(request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            AuthDTO response = await _authService.Login(request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
