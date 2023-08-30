using Microsoft.AspNetCore.Mvc;
using APINoEFCore.Services.Interface;
using APINoEFCore.Entities.RequestModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [HttpPost("createOrder")]
        [Authorize]
        public IActionResult CreateOrder(OrderRequestModel request)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.SerialNumber)?.Value;
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                if (userRole != "admin" || userId == null){
                    return Unauthorized("This user is not authorized to create an order");
                }
                
                var (success, message) = _orderService.CreateOrder(request, userId);

                if (!success){
                    return BadRequest(new { Message = message });
                }else{
                    return Ok(new { Message = message });
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions, log errors, and return a 500 status code
                return StatusCode(500, new { Message = $"An error occurred: {ex.Message}" });
            }

        }
    }
}