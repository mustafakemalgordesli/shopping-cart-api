using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Entities;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(CategoryDTO request)
        {
            ResponseDataDTO<Category> response = await _categoryService.AddAsync(request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("id")]
        [ResponseCache(Duration = 10)]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseDataDTO<Category> response = await _categoryService.GetByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet(Name = "GetAll")]
        [ResponseCache(Duration = 10)]
        public async Task<IActionResult> GetAll()
        {
            ResponseDataDTO<IEnumerable<Category>> response = await _categoryService.GetAllAsync();
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
