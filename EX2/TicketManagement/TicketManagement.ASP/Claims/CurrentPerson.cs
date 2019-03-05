using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using DataPresenter.Entity;

namespace TicketManagement.ASP.Claims
{
    [DataContract(Name = "CurrentPerson")]
    public sealed class CurrentPerson
    {
        private User user;

        public CurrentPerson(User User)
        {
            user = User;
        }

        [DataMember]
        public User User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
            }
        }
    }
}
