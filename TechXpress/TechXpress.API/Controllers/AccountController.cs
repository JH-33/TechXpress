using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechXpress.BLL.DTO.AccountDto;
using TechXpress.BLL.Manger;

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
    }
}
