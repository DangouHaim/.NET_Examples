using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.IRepository;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntityLayoutRepository : ILayoutRepository
    {
        private readonly TicketManagementContext _context;
        private readonly DbSet<Layout> _itenContext;

        public EntityLayoutRepository(TicketManagementContext context)
        {
            _context = context;
            _itenContext = context.Layout;
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

        public IQueryable<Layout> All => from x in _itenContext select x;

        public int Save(Layout layout)
        {
            _context.Entry(layout).State = EntityState.Added;
            _context.SaveChanges();
            return layout.Id;
        }

        public bool Update(Layout layout)
        {
            var r = from x in All where x.Id == layout.Id select x;
            if (r.Any())
            {
                var v = r.First();
                v.Description = layout.Description;
                v.VenueId = layout.VenueId;
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

        Layout ILayoutRepository.Get(int id)
        {
            var it = _itenContext.Single(i => i.Id == id);
            return it;
        }
    }
}
