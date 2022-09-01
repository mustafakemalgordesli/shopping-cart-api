using AutoMapper;
using WebAPI.Data.Abstract;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Exceptions;
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
        public async Task<ResponseDataDTO<ProductGetDTO>> AddAsync(ProductCreateDTO request)
        {
            ResponseDataDTO<Category> resultCategory = await _categoryService.GetByIdAsync(request.CategoryId);
            if (resultCategory.Success)
            {
                string FileName = request.File.FileName;
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + FileName;
                string imageUrl = "images/" + uniqueFileName;
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), imageUrl);
                Product product = _mapper.Map<Product>(request);
                product.ImageURL = imageUrl;
                await _unitOfWork.productDal.AddAsync(product);
                request.File.CopyTo(new FileStream(imagePath, FileMode.Create));
                await _unitOfWork.CommitAsync();
                ProductGetDTO returnedData = _mapper.Map<ProductGetDTO>(product);
                return new ResponseDataDTO<ProductGetDTO>(true, "product added", returnedData);
            }
            else
            {
                return new ResponseDataDTO<ProductGetDTO>(false, "product not added");
            }
        }

        public async Task<ResponseDataDTO<List<ProductGetDTO>>> GetAllAsync()
        {
             IEnumerable<Product> products = await _unitOfWork.productDal.GetAllAsync();
             List<ProductGetDTO> data = _mapper.Map<List<Product>, List<ProductGetDTO>>(products.ToList());
             return new ResponseDataDTO<List<ProductGetDTO>>(true, "products listed", data);   
        }

        public async Task<ResponseDataDTO<ProductGetDTO>> GetByIdAsync(int id)
        {
            Product product = await _unitOfWork.productDal.GetByIdAsync(id);
            if (product != null)
            {
                ProductGetDTO data = _mapper.Map<ProductGetDTO>(product);
                return new ResponseDataDTO<ProductGetDTO>(true, "product brought", data);
            }
            throw new NotFoundException("product not found");
        }

        public async Task<ResponseDTO> RemoveAsync(int id)
        {
            Product product = await _unitOfWork.productDal.GetByIdAsync(id);
            if (product != null)
            {
                _unitOfWork.productDal.Remove(product);
                return new ResponseDTO(true, "product deleted");
            }
            throw new NotFoundException("product not found");
        }
    }
}
