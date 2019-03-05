namespace DAL
{
    public partial class EventFile
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int EventId { get; set; }

        public Event Event { get; set; }
        public File File { get; set; }
    }
}
