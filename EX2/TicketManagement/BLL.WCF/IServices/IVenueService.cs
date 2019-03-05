using System.Collections.Generic;
using System.ServiceModel;
using DataPresenter.Entity;

namespace BLL.WCF.IServices
{
    [ServiceContract]
    public interface IVenueService
    {
        [OperationContract]
        bool Delete(int id);

        [OperationContract]
        Venue Get(int id);

        [OperationContract]
        IEnumerable<Venue> GetAll();

        [OperationContract]
        int Save(Venue venue);

        [OperationContract]
        bool Update(Venue venue);
    }
}
