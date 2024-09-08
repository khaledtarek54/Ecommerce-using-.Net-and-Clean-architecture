using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Application.DTOs;
using StackExchange.Redis;
using System.Text.Json;

namespace Ecommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IConnectionMultiplexer _redis;
        public ProductService(ILogger<ProductService> logger, IMapper mapper, IProductRepository productRepository, IConnectionMultiplexer redis)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
            _redis = redis;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };
            var cacheKey = "all_products";
            var database = _redis.GetDatabase();

            // Check if the data is already in Redis cache
            var cachedProducts = await database.StringGetAsync(cacheKey);
            if (!cachedProducts.IsNullOrEmpty)
            {
                _logger.LogInformation("returned cached products");
                return JsonSerializer.Deserialize<IEnumerable<Product>>(cachedProducts, options);
            }

            // Fetch data from the database
            var products = await _productRepository.GetAllProductsAsync();


            // Cache the data in Redis
            await database.StringSetAsync(cacheKey, JsonSerializer.Serialize(products, options), TimeSpan.FromMinutes(10));

            return products;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<Product> UpdateProductAsync(Guid id, Product product)
        {
            var productFound = await _productRepository.GetProductByIdAsync(id);
            if (productFound == null) throw new KeyNotFoundException("Product not found");
            return await _productRepository.UpdateProductAsync(id,product);
        }
        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            await _productRepository.DeleteProductAsync(product);
        }
    }
}
