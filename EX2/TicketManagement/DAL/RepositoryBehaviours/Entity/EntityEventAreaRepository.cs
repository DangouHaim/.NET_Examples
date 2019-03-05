using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.IRepository;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntityEventAreaRepository : IEventAreaRepository
    {
        private readonly TicketManagementContext _context;
        private readonly DbSet<EventArea> _itenContext;

        public EntityEventAreaRepository(TicketManagementContext context)
        {
            _context = context;
            _itenContext = context.EventArea;
        }

        public IQueryable<EventArea> All => from x in _itenContext select x;

        public bool Update(EventArea eventArea)
        {
            _context.Entry(eventArea).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        EventArea IEventAreaRepository.Get(int id)
        {
            var it = _itenContext.Single(i => i.Id == id);
            return it;
        }
    }
}
