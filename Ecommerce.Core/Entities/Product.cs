﻿using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Entities
{
    public class Product : IAuditable
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string SKU { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int StockQuantity { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public Guid BrandId { get; set; }

        public virtual Brand Brand { get; set; }

    }
}
