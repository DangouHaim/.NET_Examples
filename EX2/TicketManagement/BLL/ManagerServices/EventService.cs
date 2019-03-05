using System;
using System.Collections.Generic;
using System.Linq;
using BLL.IServices;
using DAL;
using DAL.DataEntity;
using DAL.IRepository;
using DAL.RepositoryBehaviours.Entity;

namespace BLL.ManagerServices
{
    public class EventService : IEventService
    {

        private IEventRepository Repository { get; }
        private IProcedureManager Procedures { get; }

        public EventService(IEventRepository repo, IProcedureManager manager = null)
        {
            Repository = repo;
            Procedures = manager;
        }

        public bool Delete(int id)
        {
            return Procedures.DeleteEvent(id);
        }

        public Event Get(int id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<Event> GetAll()
        {
            return Repository.All.ToList();
        }

        public IEnumerable<Event> GetForLayout(int layoutId)
        {
            return (from x in Repository.All where x.LayoutId == layoutId select x).ToList();
        }

        

        public int Save(Event _event)
        {
            return Procedures.AddEvent(_event.Name, _event.Description, _event.LayoutId, _event.EventDate);
        }

        public bool Update(Event _event)
        {
            return Procedures.UpdateEvent(_event.Id, _event.Name, _event.Description, _event.LayoutId,
                _event.EventDate);
        }

        public IEnumerable<Event> VisibleEvents(IEventAreaService eas)
        {
            var r = from x in GetAll()
                    join a in eas.GetAll() on x.Id equals a.EventId
                where a.Price != 0
                select x;
            return r.ToList();
        }

        public int TicketCount(int eventId)
        {
            return Procedures.TicketCount(eventId);
        }

        public int TicketCountTotal(int eventId)
        {
            return Procedures.TicketCountTotal(eventId);
        }

        public int Buy(int userId, int eventSeatId)
        {
            return Procedures.Buy(userId, eventSeatId);
        }

        public int ToCart(int userId, int eventSeatId)
        {

            return Procedures.ToCart(userId, eventSeatId);
        }

        public bool FromCart(int userId, int eventSeatId)
        {
            return Procedures.FromCart(userId, eventSeatId);
        }

        public int AttachFileToEvent(int eventId, string url)
        {
            return Procedures.AttachFileToEvent(eventId, url);
        }
        
        public bool DeleteFileFromEvent(int fileId)
        {
            return Procedures.DeleteFileFromEvent(fileId);
        }

        public List<string> GetAttachedFilesForEvent(int eventId)
        {
            return Procedures.GetAttachedFilesForEvent(eventId);
        }

        public List<Tuple<int, string>> GetAttachedFilesForEventToPair(int eventId)
        {
            return Procedures.GetAttachedFilesForEventToPair(eventId);
        }
    }
}
