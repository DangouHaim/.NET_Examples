using DAL.DataEntity;
using DAL.IRepository;
using System.Collections.Generic;
using System.Linq;
using BLL.IServices;
using DAL;

namespace BLL.ManagerServices
{
    public class EventSeatService : IEventSeatService
    {
        private IEventSeatRepository Repository { get; }

        public EventSeatService(IEventSeatRepository repo)
        {
            Repository = repo;
        }

        public IEnumerable<EventSeat> GetAll()
        {
            return Repository.All.ToList();
        }

        public EventSeat Get(int id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<EventSeat> GetForEventArea(int eventAreaId)
        {
            return (from x in Repository.All where x.EventAreaId == eventAreaId select x).ToList();
        }
    }
}
