using Ecommerce.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        UserRoleService _userRoleService;
        public UsersController(UserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }
        [HttpPost(Name = "/assignRole")]
        public async Task<ActionResult> AssignRole(string userid, string role)
        {
            var roleAssignResult = await _userRoleService.AssignRoleAsync(userid, role);
            return Ok(roleAssignResult);
        }

        [HttpGet]
        public async Task<ActionResult> getUsers()
        {
            return Ok(await _userRoleService.GetUsersAsync());
        }



    }
}
