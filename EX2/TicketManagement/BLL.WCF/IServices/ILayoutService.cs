using System;
using System.Collections.Generic;
using System.ServiceModel;
using DataPresenter.Entity;

namespace BLL.WCF.IServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ILayoutService" в коде и файле конфигурации.
    [ServiceContract]
    public interface ILayoutService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool Delete(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        Layout Get(int id);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<Layout> GetAll();

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int Save(Layout layout);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool Update(Layout layout);
    }
}
