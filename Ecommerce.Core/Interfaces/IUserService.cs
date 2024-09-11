using Ecommerce.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterAsync(string firstName, string lastName, string email, string password);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<bool> DeleteUserAsync(string id);

        Task<ArrayList> AuthenticateAsync(string email, string password);
        Task<string> GenerateJwtTokenAsync(User user);
    }
}
