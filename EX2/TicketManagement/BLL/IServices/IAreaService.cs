using System.Collections.Generic;
using DAL;

namespace BLL.IServices
{
    public interface IAreaService
    {
        bool Delete(int id, IEventSeatService es, IEventAreaService ea);

        Area Get(int id);

        IEnumerable<Area> GetAll();

        int Save(Area area, ILayoutService ls, ISeatService ss);

        bool Update(Area area, ILayoutService ls, ISeatService ss);
    }
}
