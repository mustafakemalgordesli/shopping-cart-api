using WebAPI.Entities;

namespace WebAPI.Data.Abstract;

public interface ICartDal : IGenericRepository<Cart>
{
    Task<Cart> GetByUserId(int id);
}
