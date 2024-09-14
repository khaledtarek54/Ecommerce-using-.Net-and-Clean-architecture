using Ecommerce.Core.Interfaces;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
