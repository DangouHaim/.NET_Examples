using System.Collections.Generic;
using DataPresenter.Entity;

namespace TicketManagement.ASP.ModelGroups
{
    public class EventGroup
    {
        public IEnumerable<Event> Events { get; set; }

        public EventGroup(IEnumerable<Event> Events)
        {
            this.Events = Events;
        }
    }
}
