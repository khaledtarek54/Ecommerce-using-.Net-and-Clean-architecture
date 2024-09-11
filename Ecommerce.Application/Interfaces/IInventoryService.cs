using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface IInventoryService
    {
        public Task ProductRestock(Order order);
        public Task ProductDeduct(Order order);
    }
}
