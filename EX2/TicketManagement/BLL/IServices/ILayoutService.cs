using System.Collections.Generic;
using DAL;

namespace BLL.IServices
{
    public interface ILayoutService
    {
        bool Delete(int id, IEventSeatService es, IEventAreaService ea);

        Layout Get(int id);

        IEnumerable<Layout> GetAll();

        int Save(Layout layout, IVenueService vs);

        bool Update(Layout layout, IVenueService vs);
    }
}
