using System.Collections.Generic;
using DAL;
using DAL.DataEntity;

namespace BLL.IServices
{
    public interface IUserService
    {
        bool Delete(int id);

        User Get(int id);

        IEnumerable<User> GetAll();

        int Save(User user);

        bool Update(User user);

        IEnumerable<User> Find(string emailOrLogin);

        IEnumerable<GetUserCart_Result3> GetCart(int uid);
    }
}
