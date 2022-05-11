using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductGetDTO>();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<IEnumerable<Product>, IEnumerable<ProductGetDTO>>();
        }
    }
}
