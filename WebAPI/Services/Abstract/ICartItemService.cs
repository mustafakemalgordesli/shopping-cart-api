using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Services.Abstract
{
    public interface ICartItemService
    {
        Task<ResponseDTO> AddAsync(CartItem item);
        Task<ResponseDTO> UpdateAsync(int id, CartItem item);
        Task<ResponseDTO> RemoveAsync(int id);
        Task<ResponseDataDTO<List<CartItem>>> GetAllByUser(int id);
        Task<ResponseDataDTO<CartItem>> GetById(int id);
    }
}
