using System.Collections.Generic;
using System.Linq;
using BLL.IServices;
using DAL;
using DAL.DataEntity;
using DAL.IRepository;

namespace BLL.ManagerServices
{
    public class EventAreaService : IEventAreaService
    {
        private IEventAreaRepository Repository { get;}

        public EventAreaService(IEventAreaRepository repo)
        {
            Repository = repo;
        }

        public IEnumerable<EventArea> GetAll()
        {
            return Repository.All.ToList();
        }

        public EventArea Get(int id)
        {
            return Repository.Get(id);
        }

        public bool Update(EventArea eventArea)
        {
            return Repository.Update(eventArea);
        }

        public IEnumerable<EventArea> GetForEvent(int eventId)
        {
            return (from x in Repository.All where x.EventId == eventId select x).ToList();
        }

        public IEnumerable<EventArea> GetForLayout(int layoutId)
        {
            return (from x in Repository.All where x.LayoutId == layoutId select x).ToList();
        }
    }
}
