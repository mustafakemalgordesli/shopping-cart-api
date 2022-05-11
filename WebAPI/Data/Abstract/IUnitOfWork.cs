namespace WebAPI.Data.Abstract;

public interface IUnitOfWork : IDisposable
{
    IProductDal productDal { get; }
    ICategoryDal categoryDal { get; }
    ICartDal cartDal { get; }
    ICartItemDal cartItemDal { get; }
    IUserDal userDal { get; }
    Task CommitAsync();
    void Commit();
}