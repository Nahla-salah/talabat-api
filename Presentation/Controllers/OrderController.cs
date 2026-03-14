using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DTO_s.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]

    public class OrderController(IServiceManager _serviceManager) : ControllerBase
    {
        //Create Order

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder([FromBody] OrderDto orderDto)
        {

            var email = User.FindFirstValue(ClaimTypes.Email);

            var order = await _serviceManager.OrderService.CreateOrderAsync(orderDto, email);

            return Ok(order);

        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<DeliveryMethodResult>> GetDeliveryMethod()
        {

            var delivery = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(delivery);
        
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Orders =await _serviceManager.OrderService.GetOrdersForUserAsync(email);
            return Ok(Orders);
        
        }


        [Authorize]
        [HttpGet("{id:guid}")]
        
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound(new { message = $"Order with ID {id} not found." });
            }
            return Ok(order);
        }

    } 
}


 
