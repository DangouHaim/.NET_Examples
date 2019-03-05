using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.IRepository;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntityAreaRepository : IAreaRepository
    {
        private readonly DbSet<Area> _itemContext;
        private readonly TicketManagementContext _context;

        public EntityAreaRepository(TicketManagementContext context)
        {
            _context = context;
            _itemContext = context.Area;
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

        public IQueryable<Area> All => from x in _itemContext select x;

        public int Save(Area area)
        {
            _context.Entry(area).State = EntityState.Added;
            _context.SaveChanges();
            return area.Id;
        }

        public bool Update(Area area)
        {
            var r = from x in All where x.Id == area.Id select x;
            if (r.Any())
            {
                var v = r.First();
                v.Description = area.Description;
                v.CoordX = area.CoordX;
                v.CoordY = area.CoordY;
                v.LayoutId = area.LayoutId;
                
                _context.Entry(v).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        Area IAreaRepository.Get(int id)
        {
            var it = _itemContext.Single(i => i.Id == id);
            return it;
        }
    }
}
