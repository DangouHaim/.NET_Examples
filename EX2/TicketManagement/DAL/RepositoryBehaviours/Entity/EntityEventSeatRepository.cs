using System.Linq;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntityEventSeatRepository : IEventSeatRepository
    {
        private TicketManagementContext _context;
        private readonly DbSet<EventSeat> _itenContext;

        public EntityEventSeatRepository(TicketManagementContext context)
        {
            _context = context;
            _itenContext = context.EventSeat;
        }

        public IQueryable<EventSeat> All => from x in _itenContext select x;

        EventSeat IEventSeatRepository.Get(int id)
        {
            var it = _itenContext.Single(i => i.Id == id);
            return it;
        }
    }
}
