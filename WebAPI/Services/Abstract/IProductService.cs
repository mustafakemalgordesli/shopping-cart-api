using WebAPI.DTOs;

namespace WebAPI.Services.Abstract;

public interface IProductService
{
    Task<ResponseDataDTO<ProductGetDTO>> AddAsync(ProductCreateDTO request);
    Task<ResponseDataDTO<List<ProductGetDTO>>> GetAllAsync();
    Task<ResponseDataDTO<ProductGetDTO>> GetByIdAsync(int id);
    Task<ResponseDTO> RemoveAsync(int id);
}
