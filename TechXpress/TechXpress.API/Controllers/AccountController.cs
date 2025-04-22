using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO.AccountDto;
using TechXpress.BLL.Manger;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechXpress.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManger _accountManager;

        public AccountController(IAccountManger accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var result = await _accountManager.Login(loginDto);
            if (result == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var result = await _accountManager.Register(registerDto);
            if (result == null)
            {
                return BadRequest("Registration failed. Please try again.");
            }
            return Ok(result);
        }
        [HttpGet("GetProfilebyid/{Id}")]
        public async Task<ActionResult<Profiledto>> GetProfilebyid(string UserId)
        {
            var profile=await _accountManager.GetProfilebyid(UserId);
            if (profile == null)
            {
                return NotFound("Profile not found.");
            }
            return Ok(profile);
        }
        [HttpDelete("DeleteProfile/{UserId)}")]
      public async Task<ActionResult<bool>> DeleteProfile(string UserId)
        {
            var result=await _accountManager.DeleteProfile(UserId);
            if (!result)
            {
                return NotFound("Profile not found.");
            }
            return NoContent();
        }
        [HttpPut("UpdateProfile/{UserId)}")]
        public async Task<ActionResult<bool>> UpdateProfile(Profiledto profiledto, string UserId)
        {
            var result=await _accountManager.UpdateProfile(profiledto, UserId);
            if (!result)
            {
                return NotFound("Profile not found.");
            }
            return NoContent();
        }
    }
}
