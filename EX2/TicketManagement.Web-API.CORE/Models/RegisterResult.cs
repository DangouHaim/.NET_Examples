using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.WebAPI.CORE.Models
{
    public class RegisterResult
    {
        public bool Successed { get; private set; }
        public string UserId { get; set; }

        public RegisterResult(bool successed = false)
        {
            Successed = successed;
        }
    }
}
