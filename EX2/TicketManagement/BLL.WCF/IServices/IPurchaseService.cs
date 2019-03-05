using System.Collections.Generic;
using System.ServiceModel;
using DataPresenter.Entity;

namespace BLL.WCF.IServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IPurchaseService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IPurchaseService
    {
        [OperationContract]
        IEnumerable<Purchase> GetAll();

        [OperationContract]
        bool Delete(int id);

        [OperationContract]
        Purchase Get(int id);

        [OperationContract]
        IEnumerable<Purchase> GetForUser(int userId);
    }
}
