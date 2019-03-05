using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.UserPostRepository
{
    public class UserPostRepository : IUserPostRepository
    {
        private readonly AppContext _context;

        public UserPostRepository(AppContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<UserPost>> All()
        {
            return await Task.Run(() => { return from x in _context.UserPosts select x; });
        }

        public async Task<bool> Delete(int id)
        {
            var r = from x in await All() where x.Id == id select x;
            if (r.Any())
            {
                _context.Entry(r.First()).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<UserPost> Get(int id)
        {
            var it = await _context.UserPosts.SingleAsync(i => i.Id == id);
            return it;
        }

        public async Task<int> Save(UserPost post)
        {
            _context.Entry(post).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return post.Id;
        }

        public async Task<bool> Update(UserPost post)
        {
            var r = from x in await All() where x.Id == post.Id select x;
            if (await r.AnyAsync())
            {
                var v = await r.FirstAsync();
                v.PostType = post.PostType;
                v.UserId = post.UserId;

                _context.Entry(v).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
