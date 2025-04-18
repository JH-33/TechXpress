﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TechXpress.BLL.DTO.AccountDto;
using TechXpress.DAL.Data.Models;

namespace TechXpress.BLL.Manger
{
    public class AccountManger : IAccountManger
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountManger(IConfiguration _configuration,UserManager<ApplicationUser>_userManager)
        {
            configuration = _configuration;
            userManager = _userManager;
        }
        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.Name);
            if (user == null)
            {
                return null;
            }

            var check = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (check == null)
            {
                return null;
            }

            var claims = await userManager.GetClaimsAsync(user);
            return GenerateToken(claims);

        }

        #region not appear details error
        //public async Task<string> Register(RegisterDto registerDto)
        //{
        //    var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
        //    if (existingUser != null)
        //    {
        //        return "Email is already registered";
        //    }
        //    ApplicationUser user = new ApplicationUser();
        //    user.Email = registerDto.Email;
        //    user.UserName = registerDto.Name;


        //    var result = await userManager.CreateAsync(user, registerDto.Password);

        //    if (result.Succeeded)
        //    {

        //        List<Claim> claims = new List<Claim>();
        //        if (registerDto.Email == "admin@example.com") 
        //        {
        //            claims.Add(new Claim("Role", "Admin"));
        //        }
        //        else
        //        {
        //            claims.Add(new Claim("Role", "User"));
        //        }

        //        claims.Add(new Claim("Name", registerDto.Name));

        //        await userManager.AddClaimsAsync(user, claims);

        //        return GenerateToken(claims);
        //    }
        //    return null;
        // } 
        #endregion
        public async Task<string> Register(RegisterDto registerDto)
        {
            var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return "Email is already registered";
            }
            ApplicationUser user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.Name,
                Address=registerDto.Address
                
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return $"Registration failed: {errors}";
            }

            List<Claim> claims = new List<Claim>
            {
        new Claim("Role", registerDto.Email == "admin@example.com" ? "Admin" : "User"),
        new Claim("Name", registerDto.Name)
             };

            await userManager.AddClaimsAsync(user, claims);

            return GenerateToken(claims);
        }

        private string GenerateToken(IList<Claim> claims)
        {
            var securitykeystring = configuration.GetSection("SecretKey").Value;
            var securtykeyByte = Encoding.ASCII.GetBytes(securitykeystring);
            SecurityKey securityKey = new SymmetricSecurityKey(securtykeyByte);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expire = DateTime.UtcNow.AddDays(2);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(claims: claims, expires: expire, signingCredentials: signingCredentials);


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string token = handler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
