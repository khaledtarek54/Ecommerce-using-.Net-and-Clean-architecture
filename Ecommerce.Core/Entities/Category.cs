using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class Category : IAuditable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } = 0;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
