using Ecommerce.Application.Services;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers.Admin
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserRoleService _userRoleService;
        private readonly IUserService _userService;

        public UsersController(UserRoleService userRoleService, IUserService userService)
        {
            _userRoleService = userRoleService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult> AssignRole(string userid, string role)
        {
            var roleAssignResult = await _userRoleService.AssignRoleAsync(userid, role);
            return Ok(roleAssignResult);
        }   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User?>>> getUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> getUserById(string id)
        {

            var user = await _userService.GetByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

    }
}
