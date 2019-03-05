namespace DAL
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int EventSeatId { get; set; }
        public int UserId { get; set; }

        public EventSeat EventSeat { get; set; }
        public User User { get; set; }
    }
}
