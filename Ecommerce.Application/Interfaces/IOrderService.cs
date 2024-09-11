using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Guid userId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId);
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task ChangeOrderStatusAsync(Guid orderId, OrderStatus orderStatus);
        Task CancelOrderAsync(Guid orderId);
    }
}
