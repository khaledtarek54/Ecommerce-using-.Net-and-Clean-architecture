using AutoMapper;
using Ecommerce.Application.DTOs;
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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public Task CancelOrderAsync(Guid orderId)
        {
            ////  change order status to cancel and return inventory
            ///
            return Task.CompletedTask;
        }

        public async Task ChangeOrderStatusAsync(Guid orderId, OrderStatus orderStatus)
        {
            var order  = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return;
            }
            order.Status = orderStatus;
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<Order> CreateOrderAsync(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            //// apply DB transaction ( urgent )*******
            //// calculate tax ( next feature )
            //// get shipping address from customer details ( next feature )
            //// get shipping cost from delivery service ( future service )
            //// adding discount ( future feature )
            //// calculate total amount of order
            //// check every product with quantity if exist inside product table or not ( urgent )*******


            Order newOrder = new Order
            {
                UserId = userId,
                Status = OrderStatus.Pending,
                TaxAmount = 50,
                ShippingAddress = "zayed",
                ShippingCost = 15,
                OrderItems = cart.CartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price,
                    TotalPrice = item.Quantity * item.Product.Price
                }).ToList(),
                TotalAmount = cart.CartItems.Sum(item => item.Quantity * item.Product.Price),

            };

            var CreatedOrder =await _orderRepository.CreateOrderAsync(newOrder);

            foreach (var orderItem in CreatedOrder.OrderItems)
            {
                orderItem.OrderId = CreatedOrder.Id;
            }

            await _orderRepository.UpdateOrderAsync(CreatedOrder); 

            //// update inventory ( urgent )********
             
            //// send mail with order details using events ( future service ) 

            return CreatedOrder;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders;
        }
    }
}
