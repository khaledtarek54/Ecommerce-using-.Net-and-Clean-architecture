using AutoMapper;
using Ecommerce.Application.Interfaces;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.Extensions.Logging;



namespace Ecommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IInventoryService inventoryService, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _inventoryService = inventoryService;
            _logger = logger;
        }

        public Task CancelOrderAsync(Guid orderId)
        {
            ////  change order status to cancel and return inventory
            ///
            return Task.CompletedTask;
        }

        public async Task ChangeOrderStatusAsync(Guid orderId, OrderStatus orderStatus)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
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
            //// calculate tax ( next feature )
            //// get shipping address from customer details ( next feature )
            //// get shipping cost from delivery service ( future service )
            //// adding discount ( future feature )
            //// calculate total amount of order


            Order createdOrder = new Order();
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

            await _orderRepository.ExecuteInTransactionAsync(async () =>
            {

                foreach (var item in newOrder.OrderItems)
                {
                    if (!await _orderRepository.ProductExistsAsync(item.ProductId, item.Quantity))
                    {
                        throw new Exception("One or more products are out of stock or do not exist.");
                    }
                }

                createdOrder = await _orderRepository.CreateOrderAsync(newOrder);

                // Set OrderId for each order item
                foreach (var orderItem in createdOrder.OrderItems)
                {
                    orderItem.OrderId = createdOrder.Id;
                }
               
                await _orderRepository.UpdateOrderAsync(createdOrder);
                _logger.LogInformation("order created");
                await _inventoryService.ProductDeduct(createdOrder);
                _logger.LogInformation("update inventory");
            });

            //// update inventory ( urgent )********

            //// send mail with order details using events ( future service ) 

            return createdOrder;
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
