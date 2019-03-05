using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.IRepository;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntitySeatRepository : ISeatRepository
    {
        private readonly TicketManagementContext _context;
        private readonly DbSet<Seat> _itenContext;

        public EntitySeatRepository(TicketManagementContext context)
        {
            _context = context;
            _itenContext = context.Seat;
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

        public IQueryable<Seat> All => from x in _itenContext select x;

        public int Save(Seat seat)
        {
            _context.Entry(seat).State = EntityState.Added;
            _context.SaveChanges();
            return seat.Id;
        }

        public bool Update(Seat seat)
        {
            var r = from x in All where x.Id == seat.Id select x;
            if (r.Any())
            {
                var v = r.First();
                v.AreaId = seat.AreaId;
                v.Number = seat.Number;
                v.Row = seat.Row;

                _context.Entry(v).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        Seat ISeatRepository.Get(int id)
        {
            var it = _itenContext.Single(i => i.Id == id);
            return it;
        }
    }
}
