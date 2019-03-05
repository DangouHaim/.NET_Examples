using System.ServiceModel;

namespace BLL.WCF.IServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IDbInitService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IDbInitService
    {
        [OperationContract]
        void Seed();
    }
}
