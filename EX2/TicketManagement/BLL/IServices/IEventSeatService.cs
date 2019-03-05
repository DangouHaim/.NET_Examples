using System.Collections.Generic;
using DAL;

namespace BLL.IServices
{
    public interface IEventSeatService
    {
        IEnumerable<EventSeat> GetAll();

        EventSeat Get(int id);

        IEnumerable<EventSeat> GetForEventArea(int eventAreaId);
    }
}
