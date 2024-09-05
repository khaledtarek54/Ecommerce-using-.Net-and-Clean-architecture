using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Product>> GetCartByUserIdAsync(Guid id)
        {
            var product = await _cartService.GetCartByUserIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddToCartAsync(Guid userId, Guid productId, int quantity)
        {
            await _cartService.AddToCartAsync(userId, productId, quantity);
            return Ok();
        }

        [HttpDelete("ClearCart/{userId}")]
        public async Task<ActionResult> ClearCartAsync(Guid userId)
        {
            await _cartService.ClearCartAsync(userId);
            return Ok();
        }
        [HttpPut("RemoveCartItem")]
        public async Task<ActionResult> RemoveFromCartAsync(Guid userId, Guid productId)
        {
            await _cartService.RemoveFromCartAsync(userId, productId);
            return Ok();
        }
    }
}
