using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ProductCreateDTO request)
        {
            ResponseDTO response = await _productService.AddAsync(request);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ResponseDataDTO<List<ProductGetDTO>> response = await _productService.GetAllAsync();
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            ResponseDataDTO<ProductGetDTO> response = await _productService.GetById(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDTO response = await _productService.Remove(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
