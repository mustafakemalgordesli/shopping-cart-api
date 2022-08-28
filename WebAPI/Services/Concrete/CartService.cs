using WebAPI.Data.Abstract;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDataDTO<Cart>> GetByUserId(int id)
        {
            try
            {
                var cart = await _unitOfWork.cartDal.GetByUserId(id);
                if (cart == null) return new ResponseDataDTO<Cart>(false, "Cart is not found");
                return new ResponseDataDTO<Cart>(true, cart);
            }
            catch (Exception)
            {
                return new ResponseDataDTO<Cart>(false, "Cart is not brought");
            }
        }
    }
}
