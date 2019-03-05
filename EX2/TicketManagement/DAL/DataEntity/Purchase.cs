using System;

namespace DAL
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public string AreaDescription { get; set; }
        public int SeatRow { get; set; }
        public int SeatNumber { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
    }
}
