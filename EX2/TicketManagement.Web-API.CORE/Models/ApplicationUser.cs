using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketManagement.WebAPI.CORE.Models
{

    public class ApplicationUser : IdentityUser
    {
        public string Password { get; set; }
    }

    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
        {

        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
