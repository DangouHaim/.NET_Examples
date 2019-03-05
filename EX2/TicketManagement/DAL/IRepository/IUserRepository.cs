using System.Linq;

namespace DAL.IRepository
{
    public interface IUserRepository
    {
        int Save(User area);//returns inserted id
        User Get(int id);
        bool Update(User area);
        bool Delete(int id);

        IQueryable<User> All { get; }
    }
}
