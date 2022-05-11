using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Services.Abstract;

public interface ICategoryService
{
    Task<ResponseDTO> AddAsync(CategoryDTO request);
    Task<ResponseDTO> UpdateAsync(int id, CategoryDTO request);
    Task<ResponseDTO> RemoveAsync(int id);
    Task<ResponseDataDTO<Category>> GetByIdAsync(int id);
    Task<ResponseDataDTO<IEnumerable<Category>>> GetAllAsync();
}
