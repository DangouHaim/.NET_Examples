using System.Collections.Generic;

namespace DAL
{
    public partial class EventSeat
    {
        public EventSeat()
        {
            Cart = new HashSet<Cart>();
        }

        public int Id { get; set; }
        public int EventAreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int State { get; set; }

        public EventArea EventArea { get; set; }
        public ICollection<Cart> Cart { get; set; }
    }
}
