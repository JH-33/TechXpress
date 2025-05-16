using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TechXpress.DAL.Data.Models;

namespace TechXpress.BLL.DTO.AccountDto
{
    public class Profiledto
    {
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string Address { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
