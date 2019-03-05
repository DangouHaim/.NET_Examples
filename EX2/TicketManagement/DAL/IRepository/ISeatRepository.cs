using System.Linq;

namespace DAL.IRepository
{
    public interface ISeatRepository
    {
        int Save(Seat seat);//returns inserted id
        Seat Get(int id);
        bool Update(Seat seat);
        bool Delete(int id);

        IQueryable<Seat> All { get; }
    }
}
