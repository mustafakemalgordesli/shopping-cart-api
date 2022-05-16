using AutoMapper;
using WebAPI.Data.Abstract;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete
{
    public class ProductService : IProductService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        ICategoryService _categoryService;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ICategoryService categoryService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _categoryService = categoryService;
        }
        public async Task<ResponseDTO> AddAsync(ProductCreateDTO request)
        {
            try
            {
                ResponseDataDTO<Category> resultCategory = await _categoryService.GetByIdAsync(request.CategoryId);
                if (resultCategory.Success)
                {
                    string FileName = request.File.FileName;
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + FileName;
                    string imageUrl = "wwwroot/images/" + uniqueFileName;
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), imageUrl);
                    Product product = _mapper.Map<Product>(request);
                    product.ImageURL = imageUrl;
                    await _unitOfWork.productDal.AddAsync(product);
                    request.File.CopyTo(new FileStream(imagePath, FileMode.Create));
                    await _unitOfWork.CommitAsync();
                    return new ResponseDTO(true, "product added");
                }
                else
                {
                    return new ResponseDTO(false, "product not added");
                }
            }
            catch (Exception)
            {
                return new ResponseDTO(false, "product not added");
            }
        }

        public async Task<ResponseDataDTO<List<ProductGetDTO>>> GetAllAsync()
        {
            try
            {
                IEnumerable<Product> products = await _unitOfWork.productDal.GetAllAsync();
                List<ProductGetDTO> data = _mapper.Map<List<Product>, List<ProductGetDTO>>(products.ToList());
                return new ResponseDataDTO<List<ProductGetDTO>>(true, "products listed", data);

            }
            catch (Exception)
            {
                return new ResponseDataDTO<List<ProductGetDTO>>(false, "products not listed");
            }
        }

        public async Task<ResponseDataDTO<ProductGetDTO>> GetById(int id)
        {
            try
            {
                Product product = await _unitOfWork.productDal.GetByIdAsync(id);
                if(product != null)
                {
                    ProductGetDTO data = _mapper.Map<ProductGetDTO>(product);
                    return new ResponseDataDTO<ProductGetDTO>(true, "product brought", data);
                }
                return new ResponseDataDTO<ProductGetDTO>(false, "product not found");

            }
            catch (Exception)
            {
                return new ResponseDataDTO<ProductGetDTO>(false, "product not brought");
            }
        }

        public async Task<ResponseDTO> Remove(int id)
        {
            try
            {
                Product product = await _unitOfWork.productDal.GetByIdAsync(id);
                if (product != null)
                {
                    _unitOfWork.productDal.Remove(product);
                    return new ResponseDTO(true, "product deleted");
                }
                return new ResponseDTO(false, "product not found");

            }
            catch (Exception)
            {
                return new ResponseDTO(false, "product not deleted");
            }
        }
    }
}
