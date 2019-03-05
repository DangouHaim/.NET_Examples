using System.Collections.Generic;
using System.ServiceModel;
using System.Web.UI.WebControls;
using DataPresenter.Entity;

namespace BLL.WCF.IServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IAreaService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IAreaService
    {
        [OperationContract]
        bool Delete(int id);

        [OperationContract]
        Area Get(int id);

        [OperationContract]
        IEnumerable<Area> GetAll();

        [OperationContract]
        int Save(Area area);

        [OperationContract]
        bool Update(Area area);
    }
}
