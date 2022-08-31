using AutoMapper;
using FluentValidation.Results;
using WebAPI.Data.Abstract;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Services.Abstract;
using WebAPI.Validations;

namespace WebAPI.Services.Concrete;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseDataDTO<Category>> AddAsync(CategoryDTO request)
    {
        Category category = _mapper.Map<Category>(request);
        CategoryValidator validator = new CategoryValidator();
        ValidationResult result = validator.Validate(category);
        if (result.IsValid)
        {
            await _unitOfWork.categoryDal.AddAsync(category);
            await _unitOfWork.CommitAsync();
            return new ResponseDataDTO<Category>(true, "category added", category);
        }
        return new ResponseDataDTO<Category>(false, result.Errors.ToString());
    }

    public async Task<ResponseDataDTO<IEnumerable<Category>>> GetAllAsync()
    {
        IEnumerable<Category> categories = await _unitOfWork.categoryDal.GetAllAsync();
        return new ResponseDataDTO<IEnumerable<Category>>(true, "categories listed", categories);
    }

    public async Task<ResponseDataDTO<Category>> GetByIdAsync(int id)
    {
        Category category = await _unitOfWork.categoryDal.GetByIdAsync(id);
        if (category != null)
        {
            return new ResponseDataDTO<Category>(true, "category brought", category);
        }
        return new ResponseDataDTO<Category>(false, "category not found");
    }

    public async Task<ResponseDTO> RemoveAsync(int id)
    {
        Category category = await _unitOfWork.categoryDal.GetByIdAsync(id);
        if (category != null)
        {
            _unitOfWork.categoryDal.Remove(category);
            _unitOfWork.Commit();
            return new ResponseDTO(true, "category deleted");
        }
        return new ResponseDTO(false, "category not found");
    }

    public async Task<ResponseDTO> UpdateAsync(int id, CategoryDTO request)
    {
        Category category = await _unitOfWork.categoryDal.GetByIdAsync(id);
        if (category != null)
        {
            category.Name = request.Name;
            _unitOfWork.categoryDal.Update(category);
            _unitOfWork.Commit();
            return new ResponseDTO(false, "category updated");
        }
        return new ResponseDTO(false, "category not found");
    }
}
