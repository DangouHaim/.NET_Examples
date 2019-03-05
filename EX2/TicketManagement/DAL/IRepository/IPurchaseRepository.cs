using System.Linq;

namespace DAL.IRepository
{
    public interface IPurchaseRepository
    {
        bool Delete(int id);

        Purchase Get(int id);

        IQueryable<Purchase> All { get; }
    }
}
