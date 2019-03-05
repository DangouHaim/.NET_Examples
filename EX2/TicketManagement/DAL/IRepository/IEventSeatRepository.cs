using System.Linq;

namespace DAL.IRepository
{
    public interface IEventSeatRepository
    {
        EventSeat Get(int id);
        
        IQueryable<EventSeat> All { get; }
    }
}
