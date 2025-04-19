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

    }
}
