using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.IRepository;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntityPurchaseRepository : IPurchaseRepository
    {
        private readonly TicketManagementContext _context;
        private readonly DbSet<Purchase> _itenContext;

        public EntityPurchaseRepository(TicketManagementContext context)
        {
            _context = context;
            _itenContext = context.Purchase;
        }

        public IQueryable<Purchase> All => from x in _itenContext select x;

        public bool Delete(int id)
        {
            var r = (from x in _context.Purchase where x.Id == id select x).First();
            if (r != null)
            {
                _context.Entry(r).State = EntityState.Deleted;
            }
            return _context.SaveChanges() > 0;
        }

        Purchase IPurchaseRepository.Get(int id)
        {
            var it = _itenContext.Single(i => i.Id == id);
            return it;
        }
    }
}
