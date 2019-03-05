using System.Linq;

namespace DAL.IRepository
{
    public interface ILayoutRepository
    {
        int Save(Layout layout);//returns inserted id
        Layout Get(int id);
        bool Update(Layout layout);
        bool Delete(int id);

        IQueryable<Layout> All { get; }
    }
}
