using WebAPI.Data.Abstract;

namespace WebAPI.Data.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly ShoppingCartDbContext _context;

    private ProductDal _productDal;
    private CategoryDal _categoryDal;
    private UserDal _userDal;
    private CartItemDal _cartItemDal;
    private CartDal _cartDal;

    public UnitOfWork(ShoppingCartDbContext context)
    {
        _context = context;
    }

    public IProductDal productDal => _productDal = _productDal ?? new ProductDal(_context);

    public ICategoryDal categoryDal => _categoryDal = _categoryDal ?? new CategoryDal(_context);
    public IUserDal userDal => _userDal = _userDal ?? new UserDal(_context);
    public ICartDal cartDal => _cartDal = _cartDal ?? new CartDal(_context);
    public ICartItemDal cartItemDal => _cartItemDal = _cartItemDal ?? new CartItemDal(_context);

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
