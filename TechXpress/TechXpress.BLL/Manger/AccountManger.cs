using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TechXpress.BLL.DTO.AccountDto;
using TechXpress.DAL.Data.Models;



namespace TechXpress.BLL.Manger
{
    public class AccountManger : IAccountManger
    {
        private readonly IConfiguration _configuration; private readonly UserManager<ApplicationUser> _userManager; private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountManger(IConfiguration configuration,
                                 UserManager<ApplicationUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AssignRuleToUser(AssignRoleDto assignRoleDto)
        {
            var user = await _userManager.FindByIdAsync(assignRoleDto.UserId);
            var role = await _roleManager.FindByIdAsync(assignRoleDto.RoleId);

            if (user == null || role == null)
                return "Invalid user or role.";

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
                return "Failed to assign role.";

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role.Name!),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

            await _userManager.AddClaimsAsync(user, claims);

            return "Role assigned successfully.";
        }

        

        public async Task<List<RoleReadDto>> GetAllRoles()
        {
            return await _roleManager.Roles
                .Select(r => new RoleReadDto { UserId = r.Id, name = r.Name })
                .ToListAsync();
        }

        public async Task<List<UserReadDto>> GetAllUser()
        {
            return await _userManager.Users
                .Select(u => new UserReadDto { Id = u.Id, name = u.UserName })
                .ToListAsync();
        }

        

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Name);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return null;

            var claims = await _userManager.GetClaimsAsync(user);
            return GenerateToken(claims);
        }

        public async Task<string> Register(RegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
                return "Email is already registered.";

            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Name,
                Address = registerDto.Address,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                List<Claim> claims = new List<Claim>
                {  new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.StreetAddress, user.Address)

                };
                await _userManager.AddClaimsAsync(user, claims);

                //var token = GenerateToken(claims);
                return result.Succeeded ? "Registered successfully." :
                   string.Join("; ", result.Errors.Select(e => e.Description)); ;
            }
            return null;

     
        }

        public async Task<bool> UpdateProfile(Profiledto profiledto)
        {
            
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                    return false;
                var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("Profile not found");

            user.UserName = profiledto.Name;
            user.PhoneNumber = profiledto.PhoneNumber;
            user.Address = profiledto.Address;
            user.Email = profiledto.Email;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> DeleteProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) throw new Exception("Profile not found");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                throw new Exception("Failed to delete profile.");

            return true;
        }

        private string GenerateToken(IList<Claim> claims)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["SecretKey"]!);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Profiledto> GetProfilebyid(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null) throw new Exception("Profile not found");

            return new Profiledto
            {
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email
            };
        }

        public async Task<string> createRole(RoleAddDto roleAddDto)
        {
            var role = new IdentityRole
            {
                Name = roleAddDto.name,
                NormalizedName = roleAddDto.name.ToUpper()
            };

            var result = await _roleManager.CreateAsync(role);

            return result.Succeeded ? "Role created successfully." : "Role creation failed.";
        }
    }

}
