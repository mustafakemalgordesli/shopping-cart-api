using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Services.Abstract
{
    public interface ICartService
    {
        Task<ResponseDTO> AddAsync(CartItem item);
        Task<ResponseDTO> UpdateAsync(int id, CartItem item);
        Task<ResponseDTO> RemoveAsync(int id);
        Task<ResponseDataDTO<Cart>> GetById(int id);
        Task<ResponseDataDTO<Cart>> GetByUserId(int id);
    }
}
