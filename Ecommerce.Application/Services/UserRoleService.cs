using Ecommerce.Core.Entities;
using Ecommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Services
{
    public class UserRoleService
    {
        private readonly UserManager<User> _userManager;
        public UserRoleService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AssignRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            if (await _userManager.IsInRoleAsync(user, role))
            {
                return IdentityResult.Failed(new IdentityError { Description = "User already has this role." });
            }

            return await _userManager.AddToRoleAsync(user,role);
        }
    }
}
