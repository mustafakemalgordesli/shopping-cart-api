using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Abstract;
using WebAPI.DTOs;
using WebAPI.Entities;

namespace WebAPI.Data.Concrete;

public class ProductDal : GenericRepository<Product>, IProductDal
{
    public ProductDal(DbContext context) : base(context)
    {
    }
}
