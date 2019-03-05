using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.IRepository;

namespace DAL.RepositoryBehaviours.Entity
{
    public class EntityUserRepository : IUserRepository
    {
        private readonly DbSet<User> _itenContext;
        private readonly TicketManagementContext _context;

        public EntityUserRepository(TicketManagementContext context)
        {
            _context = context;
            _itenContext = context.User;
        }

        public bool Delete(int id)
        {
            var r = (from x in _context.User where x.Id == id select x).ToList();
            if(r.Count == 0)
            {
                return false;
            }
            _context.Entry(r.First()).State = EntityState.Deleted;
            _context.SaveChanges();
            return true;
        }

        public IQueryable<User> All => from x in _itenContext select x;

        public int Save(User user)
        {
            var r = (from x in _context.User where x.Name == user.Name || x.Email == user.Email select x).ToList();
            if(r.Count > 0)
            {
                return -1;
            }
            _context.User.Add(user);
            _context.Entry(user).State = EntityState.Added;
            _context.SaveChanges();
            return user.Id;
        }

        public bool Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        User IUserRepository.Get(int id)
        {
            var it = _itenContext.Single(i => i.Id == id);
            return it;
        }
    }
}
