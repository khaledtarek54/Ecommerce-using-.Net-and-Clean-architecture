using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>>GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Guid id, Product product);
        Task DeleteProductAsync(Guid id);
    }
}
