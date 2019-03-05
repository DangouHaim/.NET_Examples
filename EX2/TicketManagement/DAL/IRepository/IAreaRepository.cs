using System.Linq;

namespace DAL.IRepository
{
    public interface IAreaRepository
    {
        int Save(Area area);//returns inserted id
        Area Get(int id);
        bool Update(Area area);
        bool Delete(int id);

        IQueryable<Area> All { get; }
    }
}
