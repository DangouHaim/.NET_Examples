using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using BLL.WCF.IServices;
using DataPresenter.Entity;
using DAL.RepositoryBehaviours.Entity;

namespace BLL.WCF
{
    public class EventService : IEventService
    {
        private readonly BLL.IServices.IEventService _service;
        private readonly BLL.IServices.IEventAreaService _eventAreaService;
        private readonly BLL.IServices.IEventSeatService _eventSeatService;

        public EventService()
        {
            DAL.TicketManagementContext context = new DAL.TicketManagementContext();
            _service = new ManagerServices.EventService(new EntityEventRepository(context), new EntityProcedureManager(context));
            _eventAreaService = new ManagerServices.EventAreaService(new EntityEventAreaRepository(new DAL.TicketManagementContext()));
            _eventSeatService = new ManagerServices.EventSeatService(new EntityEventSeatRepository(context));
        }

        public bool Delete(int id)
        {
            return _service.Delete(id);
        }

        public Event Get(int id)
        {
            return Event.GetFromEntity(_service.Get(id));
        }

        public IEnumerable<Event> GetAll()
        {
            return Event.GetFromEntityList(_service.GetAll());
        }

        public IEnumerable<Event> GetForLayout(int layoutId)
        {
            return Event.GetFromEntityList(_service.GetForLayout(layoutId));
        }

        public IEnumerable<Event> VisibleEvents()
        {
            return Event.GetFromEntityList(_service.VisibleEvents(_eventAreaService));
        }

        public int Save(Event _event)
        {
            return _service.Save(_event.ToEntity());
        }

        public bool Update(Event _event)
        {
            return _service.Update(_event.ToEntity());
        }

        public int TicketCount(int eventId)
        {
            return _service.TicketCount(eventId);
        }

        public int TicketCountTotal(int eventId)
        {
            return _service.TicketCountTotal(eventId);
        }

        public int Buy(int userId, int eventSeatId)
        {
            return _service.Buy(userId, eventSeatId);
        }

        public int ToCart(int userId, int eventSeatId)
        {
            return _service.ToCart(userId, eventSeatId);
        }

        public bool FromCart(int userId, int eventSeatId)
        {
            return _service.FromCart(userId, eventSeatId);
        }

        public int AttachFileToEvent(int eventId, string url)
        {
            return _service.AttachFileToEvent(eventId, url);
        }

        public bool DeleteFileFromEvent(int fileId)
        {
            return _service.DeleteFileFromEvent(fileId);
        }

        public List<string> GetAttachedFilesForEvent(int eventId)
        {
            return _service.GetAttachedFilesForEvent(eventId);
        }

        public List<Tuple<int, string>> GetAttachedFilesForEventToPair(int eventId)
        {
            return _service.GetAttachedFilesForEventToPair(eventId);
        }
    }
}
