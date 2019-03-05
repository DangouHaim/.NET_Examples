using System.Collections.Generic;
using System.ServiceModel;
using DataPresenter.Entity;

namespace BLL.WCF.IServices
{
    [ServiceContract]
    public interface IEventSeatService
    {
        [OperationContract]
        IEnumerable<EventSeat> GetAll();

        [OperationContract]
        EventSeat Get(int id);

        [OperationContract]
        IEnumerable<EventSeat> GetForEventArea(int eventAreaId);
    }
}
