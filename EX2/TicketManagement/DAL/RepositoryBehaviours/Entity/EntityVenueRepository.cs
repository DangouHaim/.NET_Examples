using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.IRepository;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntityVenueRepository : IVenueRepository
    {
        private readonly TicketManagementContext _context;
        private readonly DbSet<Venue> _itenContext;

        public EntityVenueRepository(TicketManagementContext context)
        {
            _context = context;
            _itenContext = context.Venue;
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

        public IQueryable<Venue> All => from x in _itenContext select x;

        public int Save(Venue venue)
        {
            _context.Entry(venue).State = EntityState.Added;
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return -1;
            }
            return venue.Id;
        }

        public bool Update(Venue venue)
        {
            var r = from x in All where x.Id == venue.Id select x;
            if (r.Any())
            {
                var v = r.First();
                v.Description = venue.Description;
                v.Address = venue.Address;
                v.Phone = venue.Phone;
                _context.Entry(v).State = EntityState.Modified;
                try
                {
                    _context.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        Venue IVenueRepository.Get(int id)
        {
            var it = _itenContext.Single(i => i.Id == id);
            return it;
        }
    }
}
