using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Abstract;
using WebAPI.Entities;

namespace WebAPI.Data.Concrete
{
    public class CartDal : GenericRepository<Cart>, ICartDal
    {
        public CartDal(DbContext context) : base(context)
        {
        }
    }
}
