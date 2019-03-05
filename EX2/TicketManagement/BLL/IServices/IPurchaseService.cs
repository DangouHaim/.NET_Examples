using System.Collections.Generic;
using DAL;

namespace BLL.IServices
{
    public interface IPurchaseService
    {
        IEnumerable<Purchase> GetAll();

        bool Delete(int id);

        Purchase Get(int id);

        IEnumerable<Purchase> GetForUser(int userId);
    }
}
