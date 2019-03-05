using System.Collections.Generic;
using System.Linq;
using BLL.IServices;
using DAL;
using DAL.DataEntity;
using DAL.IRepository;

namespace BLL.ManagerServices
{
    public class UserService : IUserService
    {
        private IUserRepository Repository { get; }
        private IProcedureManager Procedures { get; }

        public UserService(IUserRepository repo, IProcedureManager manager)
        {
            Repository = repo;
            Procedures = manager;
        }

        public bool Delete(int id)
        {
            return Repository.Delete(id);
        }

        public User Get(int id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<User> GetAll()
        {
            return Repository.All.ToList();
        }

        public int Save(User user)
        {
            return Repository.Save(user);
        }

        public bool Update(User user)
        {
            return Repository.Update(user);
        }

        public IEnumerable<User> Find(string emailOrLogin)
        {
            return (from x in Repository.All where x.Name == emailOrLogin || x.Email == emailOrLogin select x).ToList();
        }

        public IEnumerable<GetUserCart_Result3> GetCart(int uid)
        {
            return Procedures.GetCart(uid);
        }
    }
}
