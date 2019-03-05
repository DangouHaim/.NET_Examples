using System.Collections.Generic;
using System.ServiceModel;
using DataPresenter.Entity;

namespace BLL.WCF.IServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IEventAreaService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IEventAreaService
    {
        [OperationContract]
        IEnumerable<EventArea> GetAll();

        [OperationContract]
        EventArea Get(int id);

        [OperationContract]
        bool Update(EventArea eventArea);

        [OperationContract]
        IEnumerable<EventArea> GetForEvent(int eventId);

        [OperationContract]
        IEnumerable<EventArea> GetForLayout(int layoutId);
    }
}
