using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.BLL.DTO.AccountDto;

namespace TechXpress.BLL.Manger
{
    public interface IAccountManger
    {
        Task<string> Login(LoginDto loginDto);
        Task<string> Register(RegisterDto registerDto);
        Task<List<UserReadDto>>GetAllUser();
        Task<Profiledto> GetProfilebyid(String UserId);
        Task<bool> DeleteProfile(String UserId);
        Task<bool> UpdateProfile(Profiledto profiledto, String UserId);
        Task<string> createRole(RoleAddDto roleAddDto);
        Task<string> AssignRuleToUser(AssignRoleDto assignRoleDto);
        Task<List<RoleReadDto>> GetAllRoles( );


         
    }
}
