using System.Collections.Generic;
using System.ServiceModel;
using DataPresenter.Entity;

namespace BLL.WCF.IServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IUserService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool Delete(int id);

        [OperationContract]
        User Get(int id);

        [OperationContract]
        IEnumerable<User> GetAll();

        [OperationContract]
        int Save(User user);

        [OperationContract]
        bool Update(User user);

        [OperationContract]
        IEnumerable<User> Find(string emailOrLogin);

        [OperationContract]
        IEnumerable<GetUserCart_Result3> GetCart(int uid);
    }
}
