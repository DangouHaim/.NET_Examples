using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagement.ASP.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string EMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password error")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
