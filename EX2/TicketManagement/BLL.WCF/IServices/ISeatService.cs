using System.Collections.Generic;
using System.ServiceModel;
using DataPresenter.Entity;

namespace BLL.WCF.IServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ISeatService" в коде и файле конфигурации.
    [ServiceContract]
    public interface ISeatService
    {
        [OperationContract]
        bool Delete(int id);

        [OperationContract]
        Seat Get(int id);

        [OperationContract]
        IEnumerable<Seat> GetAll();

        [OperationContract]
        int Save(Seat seat);

        [OperationContract]
        bool Update(Seat seat);
    }
}
