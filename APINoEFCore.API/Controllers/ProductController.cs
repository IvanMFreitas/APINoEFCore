using APINoEFCore.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APINoEFCore.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}