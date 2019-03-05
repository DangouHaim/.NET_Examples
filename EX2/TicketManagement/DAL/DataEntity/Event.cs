using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Event
    {
        public Event()
        {
            EventArea = new HashSet<EventArea>();
            EventFile = new HashSet<EventFile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LayoutId { get; set; }
        public DateTime EventDate { get; set; }

        public Layout Layout { get; set; }
        public ICollection<EventArea> EventArea { get; set; }
        public ICollection<EventFile> EventFile { get; set; }
    }
}
