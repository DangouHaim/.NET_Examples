using System.Collections.Generic;
using DAL;

namespace BLL.IServices
{
    public interface IVenueService
    {
        bool Delete(int id, IEventSeatService ess, IEventAreaService eas, ILayoutService ls);

        Venue Get(int id);

        IEnumerable<Venue> GetAll();

        int Save(Venue venue);

        bool Update(Venue venue);
    }
}
