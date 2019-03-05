using System.Collections.Generic;
using System.Linq;

namespace DAL.IRepository
{
    public interface IEventRepository
    {
        int Save(Event _event);//returns inserted id
        Event Get(int id);
        bool Update(Event _event);
        bool Delete(int id);

        IQueryable<Event> All { get; }
    }
}
