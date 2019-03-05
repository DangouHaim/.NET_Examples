using System.Linq;

namespace DAL.IRepository
{
    public interface IVenueRepository
    {
        int Save(Venue venue);//returns inserted id
        Venue Get(int id);
        bool Update(Venue venue);
        bool Delete(int id);

        IQueryable<Venue> All { get; }
    }
}
