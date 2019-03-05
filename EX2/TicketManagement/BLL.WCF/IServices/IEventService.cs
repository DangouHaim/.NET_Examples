using System;
using System.Collections.Generic;
using System.ServiceModel;
using DataPresenter.Entity;

namespace BLL.WCF.IServices
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IEventService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IEventService
    {
        [OperationContract]
        bool Delete(int id);

        [OperationContract]
        Event Get(int id);

        [OperationContract]
        IEnumerable<Event> GetAll();

        [OperationContract]
        IEnumerable<Event> GetForLayout(int layoutId);

        [OperationContract]
        IEnumerable<Event> VisibleEvents();

        [OperationContract]
        int Save(Event _event);

        [OperationContract]
        bool Update(Event _event);

        [OperationContract]
        int TicketCount(int eventId);

        [OperationContract]
        int TicketCountTotal(int eventId);

        [OperationContract]
        int Buy(int userId, int eventSeatId);

        [OperationContract]
        int ToCart(int userId, int eventSeatId);

        [OperationContract]
        bool FromCart(int userId, int eventSeatId);

        [OperationContract]
        int AttachFileToEvent(int eventId, string url);

        [OperationContract]
        bool DeleteFileFromEvent(int fileId);

        [OperationContract]
        List<string> GetAttachedFilesForEvent(int eventId);

        [OperationContract]
        List<Tuple<int, string>> GetAttachedFilesForEventToPair(int eventId);
    }
}
