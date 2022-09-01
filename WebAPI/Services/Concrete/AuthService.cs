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
using WebAPI.Utils;
using WebAPI.Validations;

namespace WebAPI.Services.Concrete;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private IJwtUtils _jwtUtils;
    private IMapper _mapper;
    private ICipherUtils _cipherUtils;

    public AuthService(IUnitOfWork unitOfWork, IJwtUtils jwtUtils, IMapper mapper, ICipherUtils cipherUtils)
    {
        _unitOfWork = unitOfWork;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
        _cipherUtils = cipherUtils;
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
                        string token = _cipherUtils.Encrypt(_jwtUtils.GenerateToken(user));
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
                string token = _cipherUtils.Encrypt(_jwtUtils.GenerateToken(user));
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
