using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechXpress.BLL.DTO.AccountDto;
using TechXpress.BLL.Manger;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManger _accountManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAccountManger accountManager, IHttpContextAccessor httpContextAccessor)
        {
            _accountManager = accountManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginDto loginDto)
        {
            var result = await _accountManager.Login(loginDto);
            if (string.IsNullOrEmpty(result))
                return Unauthorized("Invalid username or password.");

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register(RegisterDto registerDto)
        {
            var result = await _accountManager.Register(registerDto);
            if (string.IsNullOrEmpty(result))
                return BadRequest("Registration failed. Please try again.");

            return Ok(result);
        }

        //[Authorize]
        [HttpGet("GetProfile")]
        public async Task<ActionResult<Profiledto>> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User ID not found in token.");

            var profile = await _accountManager.GetProfilebyid(userId);
            if (profile == null)
                return NotFound("Profile not found.");

            return Ok(profile);
        }

        //[Authorize]
        [HttpDelete("DeleteProfile")]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User ID not found in token.");

            var result = await _accountManager.DeleteProfile(userId);
            if (!result)
                return NotFound("Profile not found or could not be deleted.");

            return NoContent();
        }

       
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(Profiledto profiledto)
        {
           
            var result = await _accountManager.UpdateProfile(profiledto);
            if (!result)
                return NotFound("Profile not found or could not be updated.");

            return NoContent();
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("CreateRole")]
        public async Task<ActionResult<string>> CreateRole(RoleAddDto roleAddDto)
        {
            var result = await _accountManager.createRole(roleAddDto);
            if (string.IsNullOrEmpty(result))
                return BadRequest("Role creation failed.");

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<List<RoleReadDto>>> GetAllRoles()
        {
            var roles = await _accountManager.GetAllRoles();
            return Ok(roles);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserReadDto>>> GetAllUsers()
        {
            var users = await _accountManager.GetAllUser();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AssignRoleToUser")]
        public async Task<ActionResult<string>> AssignRoleToUser(AssignRoleDto assignRoleDto)
        {
            var result = await _accountManager.AssignRuleToUser(assignRoleDto);
            if (string.IsNullOrEmpty(result))
                return BadRequest("Assigning role failed.");

            return Ok(result);
        }
    }
}
