using Microsoft.AspNetCore.Mvc;
using APINoEFCore.Services.Interface;

namespace APINoEFCore.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(Guid id)
        {
            try
            {
                var order = _orderService.GetById(id);

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}