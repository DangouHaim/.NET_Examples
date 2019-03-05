using System.Collections.Generic;

namespace DAL
{
    public partial class File
    {
        public File()
        {
            EventFile = new HashSet<EventFile>();
        }

        public int Id { get; set; }
        public string Url { get; set; }

        public ICollection<EventFile> EventFile { get; set; }
    }
}
