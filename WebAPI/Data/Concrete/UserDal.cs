using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Abstract;
using WebAPI.Entities;

namespace WebAPI.Data.Concrete;

public class UserDal : GenericRepository<User>, IUserDal
{
    public UserDal(DbContext context) : base(context)
    {
    }
}
