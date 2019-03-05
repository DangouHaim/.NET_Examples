using System.Collections.Generic;
using DAL;

namespace BLL.IServices
{
    public interface IEventAreaService
    {
        IEnumerable<EventArea> GetAll();

        EventArea Get(int id);

        bool Update(EventArea eventArea);

        IEnumerable<EventArea> GetForEvent(int eventId);

        IEnumerable<EventArea> GetForLayout(int layoutId);
    }
}
