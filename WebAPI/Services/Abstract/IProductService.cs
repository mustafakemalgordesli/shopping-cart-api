using WebAPI.DTOs;

namespace WebAPI.Services.Abstract;

public interface IProductService
{
    Task<ResponseDTO> AddAsync(ProductCreateDTO request);
    Task<ResponseDataDTO<IEnumerable<ProductGetDTO>>> GetAllAsync();
    Task<ResponseDataDTO<ProductGetDTO>> GetById(int id);
    Task<ResponseDTO> Remove(int id);
}
