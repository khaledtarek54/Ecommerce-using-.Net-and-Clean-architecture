using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("createOrder/{userId}")]
        public async Task<ActionResult<Order>> CreateOrder(Guid userId)
        {
            var createdOrder = await _orderService.CreateOrderAsync(userId);
            return Ok(createdOrder);
        }
        [HttpPost("ChangeOrderStatus")]
        public async Task<ActionResult<Order>> ChangeOrderStatus(Guid orderId, OrderStatus orderStatus)
        {
            await _orderService.ChangeOrderStatusAsync(orderId,orderStatus);
            return Ok();
        }

        [HttpGet("OrderByOrderId/{orderId}")]
        public async Task<ActionResult<Order>> GetOrderByOrderId(Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpGet("OrdersByUserId/{userId}")]
        public async Task<ActionResult<Order>> GetOrdersByUserId(Guid userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

    }
}
