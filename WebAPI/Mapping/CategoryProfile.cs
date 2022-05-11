using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDTO, Category>();
        }
    }
}
