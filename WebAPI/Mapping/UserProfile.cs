using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>();
    }
}
