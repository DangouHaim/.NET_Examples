using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.WebAPI.CORE.Models
{
    public class UserInfo
    {
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
