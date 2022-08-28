using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Services.Abstract
{
    public interface ICartService
    {
        Task<ResponseDataDTO<Cart>> GetByUserId(int id);
    }
}
