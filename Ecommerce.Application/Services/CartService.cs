using Ecommerce.Application.Interfaces;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task AddToCartAsync(Guid userId, Guid productId, int quantity)
        {
            await _cartRepository.AddToCartAsync(userId, productId, quantity);
        }

        public async Task ClearCartAsync(Guid userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid userId)
        {
           return await _cartRepository.GetCartByUserIdAsync(userId);
        }

        public async Task RemoveFromCartAsync(Guid userId, Guid productId)
        {
            await _cartRepository.RemoveFromCartAsync(userId, productId);
        }
    }
}
