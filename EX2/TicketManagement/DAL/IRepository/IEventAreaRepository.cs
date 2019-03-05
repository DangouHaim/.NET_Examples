using System.Linq;

namespace DAL.IRepository
{
    public interface IEventAreaRepository
    {
        EventArea Get(int id);
        bool Update(EventArea eventArea);

        IQueryable<EventArea> All { get; }
    }
}
