using System.Linq;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntityEventRepository : IEventRepository
    {
        private readonly TicketManagementContext _context;
        private readonly DbSet<Event> _itenContext;

        public EntityEventRepository(TicketManagementContext context)
        {
            _context = context;
            _itenContext = context.Event;
        }

        public bool Delete(int id)
        {
            var r = from x in All where x.Id == id select x;
            if (r.Any())
            {
                _context.Entry(r.First()).State = EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Event> All => from x in _itenContext select x;

        public int Save(Event @event)
        {
            _context.Entry(@event).State = EntityState.Added;
            _context.SaveChanges();
            return @event.Id;
        }

        public bool Update(Event Event)
        {
            _context.Entry(Event).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        Event IEventRepository.Get(int id)
        {
            return (from @event in All where @event.Id == id select @event).First();
        }
    }
}
