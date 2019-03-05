using DAL.DataEntity;
using DAL.IRepository;
using System.Collections.Generic;
using System.Linq;
using BLL.IServices;
using DAL;

namespace BLL.ManagerServices
{
    public class PurchaseService : IPurchaseService
    {
        private IPurchaseRepository Repository { get; }

        public PurchaseService(IPurchaseRepository repo)
        {
            Repository = repo;
        }

        public IEnumerable<Purchase> GetAll()
        {
            return Repository.All.ToList();
        }

        public Purchase Get(int id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<Purchase> GetForUser(int userId)
        {
            return (from x in Repository.All where x.UserId == userId select x).ToList();
        }

        public bool Delete(int id)
        {
            return Repository.Delete(id);
        }
    }
}
