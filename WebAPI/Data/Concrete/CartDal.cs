using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Abstract;
using WebAPI.Entities;

namespace WebAPI.Data.Concrete
{
    public class CartDal : GenericRepository<Cart>, ICartDal
    {
        DbContext _context;
        public CartDal(DbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cart> GetByUserId(int id)
        {
            Cart cart = await _context.Set<Cart>().SingleOrDefaultAsync(x => x.UserId == id);
            return cart;
        }
    }
}
