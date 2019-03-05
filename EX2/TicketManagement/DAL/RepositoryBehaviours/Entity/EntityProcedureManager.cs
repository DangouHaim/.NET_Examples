using DAL.IRepository;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.DataEntity;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntityProcedureManager : IProcedureManager
    {
        private readonly TicketManagementContext _context;

        public EntityProcedureManager(TicketManagementContext context)
        {
            _context = context;
        }

        public int Buy(int userId, int eventSeatId)
        {
            return _context.Buy(userId, eventSeatId);
        }

        public int ToCart(int userId, int eventSeatId)
        {
            return _context.ToCart(userId, eventSeatId);
        }

        public bool FromCart(int userId, int eventSeatId)
        {
            return _context.FromCart(userId, eventSeatId);
        }

        public int AttachFileToEvent(int eventId, string url)
        {
            return _context.AttachFileToEvent(eventId, url);
        }

        public bool DeleteFileFromEvent(int fileId)
        {
            var r = (from x in _context.File where x.Id == fileId select x).First();
            if (r != null)
            {
                _context.Entry(r).State = EntityState.Deleted;
            }
            return _context.SaveChanges() > 0;
        }

        public List<string> GetAttachedFilesForEvent(int eventId)
        {
            var r = _context.GetAttachedFilesForEvent(eventId);
            List<string> res = new List<string>();
            
            if(r != null && r.Any())
            {
                foreach (var file in r)
                {
                    res.Add(file.Url);
                }
            }
            return res;
        }

        public List<Tuple<int, string>> GetAttachedFilesForEventToPair(int eventId)
        {
            List<Tuple<int, string>> r = new List<Tuple<int, string>>();

            var urls = GetAttachedFilesForEvent(eventId);

            var ids = (from x in _context.EventFile where x.EventId == eventId select x.Id).ToList();

            var mapToPair = ids.Zip(urls, (id, url) => new { id, url });

            foreach (var x in mapToPair)
            {
                r.Add(new Tuple<int, string>(x.id, x.url));
            }
            return r;
        }

        public IEnumerable<GetUserCart_Result3> GetCart(int uid)
        {
            var r = from x in _context.GetCart(uid)
                join eventSeat in _context.EventSeat on x.EventSeatId equals eventSeat.Id
                join eventArea in _context.EventArea on eventSeat.EventAreaId equals eventArea.Id
                join layout in _context.Layout on eventArea.LayoutId equals layout.Id
                join venue in _context.Venue on layout.VenueId equals venue.Id
                where x.UserId == uid
                select new GetUserCart_Result3()
                {
                    Number = eventSeat.Number,
                    Row = eventSeat.Row,
                    EventSeatId = eventSeat.Id,
                    Address = venue.Address,
                    AreaDescription = eventArea.Description,
                    LayoutDescription = layout.Description,
                    Phone = venue.Phone,
                    Price = eventArea.Price,
                    VenueDescription = venue.Description
                };
            return r.ToList();
        }

        public int AddEvent(string name, string description, int layoutId, DateTime eventDate)
        {
            return _context.AddEvent(name, description, layoutId, eventDate);
        }

        public bool UpdateEvent(int eventId, string name, string description, int layoutId, DateTime eventDate)
        {
            return _context.UpdateEvent(eventId, name, description, layoutId, eventDate);
        }

        public bool DeleteEvent(int eventId)
        {
            return _context.DeleteEvent(eventId);
        }

        public int TicketCount(int eventId)
        {
            return _context.TicketCount(eventId);
        }

        public int TicketCountTotal(int eventId)
        {
            return _context.TicketCountTotal(eventId);
        }
    }
}
