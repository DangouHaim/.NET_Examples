using System.Collections.Generic;
using BLL.ManagerServices;
using DAL;

namespace BLL.IServices
{
    public interface ISeatService
    {
        bool Delete(int id, IAreaService a, IEventSeatService es, IEventAreaService ea);

        Seat Get(int id);

        IEnumerable<Seat> GetAll();

        int Save(Seat seat, IAreaService @as);

        bool Update(Seat seat, IAreaService @as);
    }
}
