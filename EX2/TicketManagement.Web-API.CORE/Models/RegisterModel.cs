using System.ComponentModel.DataAnnotations;

namespace TicketManagement.WebAPI.CORE.Models
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
