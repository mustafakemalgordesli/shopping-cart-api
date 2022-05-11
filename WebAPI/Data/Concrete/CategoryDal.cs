using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Abstract;
using WebAPI.Entities;

namespace WebAPI.Data.Concrete;

public class CategoryDal : GenericRepository<Category>, ICategoryDal
{
    public CategoryDal(DbContext context) : base(context)
    {
    }
}
