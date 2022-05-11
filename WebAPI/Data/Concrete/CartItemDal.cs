using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Abstract;
using WebAPI.Entities;

namespace WebAPI.Data.Concrete
{
    public class CartItemDal : GenericRepository<CartItem>, ICartItemDal
    {
        public CartItemDal(DbContext context) : base(context)
        {
        }
    }
}
