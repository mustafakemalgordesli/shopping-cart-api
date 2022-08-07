using AutoMapper;
using FluentValidation.Results;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using WebAPI.Data.Abstract;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Services.Abstract;
using WebAPI.Validations;

namespace WebAPI.Services.Concrete;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private IConfiguration _configuration;
    private IMapper _mapper;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mapper = mapper;
    }
    public async Task<AuthDTO> Login(LoginDTO request)
    {
        try
        {
            LoginDtoValidator validator = new LoginDtoValidator();
            ValidationResult result = validator.Validate(request);
            if(result.IsValid)
            {
                User user = await _unitOfWork.userDal.SingleOrDefaultAsync(u => u.Email == request.Email);
                if (user != null)
                {
                    if (VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                    {
                        string token = CreateToken(user);
                        UserDTO userDTO = _mapper.Map<UserDTO>(user);
                        AuthDTO response = new AuthDTO(true, "auth successfull", token, userDTO);
                        return response;
                    }
                    else
                    {
                        return new AuthDTO(false, "auth failed");
                    }
                }
                else
                {
                    return new AuthDTO(false, "auth failed");
                }
            }
            return new AuthDTO(false, "auth failed");

        }
        catch (Exception)
        {
            return new AuthDTO(false, "auth failed");
        }
    }

    public async Task<AuthDTO> Register(RegisterDTO request)
    {
        try
        {
            RegisterDtoValidator validator = new RegisterDtoValidator();
            ValidationResult result = validator.Validate(request);
            if (result.IsValid)
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                User user = new User()
                {
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.FirstName,
                    LastName = request?.LastName,
                    Email = request.Email,
                    Status = true,
                };
                await _unitOfWork.userDal.AddAsync(user);
                await _unitOfWork.CommitAsync();
                await _unitOfWork.cartDal.AddAsync(new Cart()
                {
                    UserId = user.Id
                });
                await _unitOfWork.CommitAsync();
                string token = CreateToken(user);
                UserDTO userDTO = _mapper.Map<UserDTO>(user);
                AuthDTO response = new AuthDTO(true, "user registration successfully created", token, userDTO);
                return response;
            }
            return new AuthDTO(false, "failed to create user registration");
        }
        catch (Exception)
        {
            return new AuthDTO(false, "failed to create user registration");
        }
    }


    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Trim()),
            };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
