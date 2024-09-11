using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(Guid userId);
        Task AddToCartAsync(Guid userId, Guid productId, int quantity);
        Task RemoveFromCartAsync(Guid userId, Guid productId);
        Task ClearCartAsync(Guid userId);
    }
}
