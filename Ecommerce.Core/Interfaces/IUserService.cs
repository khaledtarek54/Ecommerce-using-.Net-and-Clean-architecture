using Ecommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string firstName, string lastName, string email, string password);
        Task<User> AuthenticateAsync(string email, string password);
        string GenerateJwtToken(User user);
    }
}
