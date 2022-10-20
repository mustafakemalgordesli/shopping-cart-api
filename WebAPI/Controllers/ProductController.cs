using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;
using WebAPI.DTOs;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpPost]
        [ServiceFilter(typeof(LoginFilter))]
        [ClaimRequirementFilter(Roles = new string[] { "Admin", "SuperAdmin" })]
        public async Task<IActionResult> Add([FromForm] ProductCreateDTO request)
        {
            ResponseDataDTO<ProductGetDTO> response = await _productService.AddAsync(request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        
        [HttpGet]
        [ResponseCache(Duration = 10)]
        public async Task<IActionResult> GetAll()
        {
            ResponseDataDTO<List<ProductGetDTO>> response = await _productService.GetAllAsync();
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseDataDTO<ProductGetDTO> response = await _productService.GetByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDTO response = await _productService.RemoveAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
