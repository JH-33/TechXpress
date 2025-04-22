using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.BLL.DTO.AccountDto
{
    public class RegisterDto
    {
        public string Name { get; set; }

        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        [Compare("PhoneNumber")]
        public string ConfirmPhoneNumber { get; set; }
    }
}
