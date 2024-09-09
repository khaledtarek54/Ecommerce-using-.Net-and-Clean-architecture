using Ecommerce.Application.Interfaces;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(IProductRepository productRepository, ILogger<InventoryService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task ProductDeduct(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {

                var product = _productRepository.GetProductByIdAsync(orderItem.ProductId);
                if (product != null)
                {
                    product.Result.StockQuantity -= orderItem.Quantity;
                    await _productRepository.UpdateProductAsync(product.Result.Id, product.Result);
                }
            }
        }


        public async Task ProductRestock(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {

                var product = _productRepository.GetProductByIdAsync(orderItem.ProductId);
                if (product != null)
                {
                    product.Result.StockQuantity += orderItem.Quantity;
                    await _productRepository.UpdateProductAsync(product.Result.Id, product.Result);

                }
            }
        }
    }
}
