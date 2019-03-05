using System;
using System.Collections.Generic;
using DAL.DataEntity;

namespace DAL.IRepository
{
    public interface IProcedureManager
    {
        int Buy(int userId, int eventSeatId);
        int ToCart(int userId, int eventSeatId);
        bool FromCart(int userId, int eventSeatId);
        int AttachFileToEvent(int eventId, string url);
        bool DeleteFileFromEvent(int fileId);
        List<string> GetAttachedFilesForEvent(int eventId);
        List<Tuple<int, string>> GetAttachedFilesForEventToPair(int eventId);
        IEnumerable<GetUserCart_Result3> GetCart(int uid);
        int AddEvent(string name, string description, int layoutId, DateTime eventDate);
        bool UpdateEvent(int eventId, string name, string description, int layoutId, DateTime eventDate);
        bool DeleteEvent(int eventId);
        int TicketCount(int eventId);
        int TicketCountTotal(int eventId);
    }
}
