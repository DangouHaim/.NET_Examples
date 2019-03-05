using System;
using System.Collections.Generic;
using DAL;

namespace BLL.IServices
{
    public interface IEventService
    {
        bool Delete(int id);

        Event Get(int id);

        IEnumerable<Event> GetAll();

        IEnumerable<Event> GetForLayout(int layoutId);

        IEnumerable<Event> VisibleEvents(IEventAreaService eas);

        int Save(Event _event);

        bool Update(Event _event);

        int TicketCount(int eventId);

        int TicketCountTotal(int eventId);

        int Buy(int userId, int eventSeatId);

        int ToCart(int userId, int eventSeatId);

        bool FromCart(int userId, int eventSeatId);

        int AttachFileToEvent(int eventId, string url);

        bool DeleteFileFromEvent(int fileId);

        List<string> GetAttachedFilesForEvent(int eventId);

        List<Tuple<int, string>> GetAttachedFilesForEventToPair(int eventId);
    }
}
