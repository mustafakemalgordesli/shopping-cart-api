using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;
using WebAPI.DTOs;
using WebAPI.Services.Abstract;
using WebAPI.Utils;

namespace WebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        ILoggerManager _logger;
        public ProductController(IProductService productService, ILoggerManager logger)
        {
            _productService = productService;
            _logger = logger;
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
            _logger.LogInfo("Products.GetAll() has been run");
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
