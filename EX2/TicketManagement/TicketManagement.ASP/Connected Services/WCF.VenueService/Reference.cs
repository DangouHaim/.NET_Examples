//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketManagement.ASP.WCF.VenueService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCF.VenueService.IVenueService")]
    public interface IVenueService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/Delete", ReplyAction="http://tempuri.org/IVenueService/DeleteResponse")]
        bool Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/Delete", ReplyAction="http://tempuri.org/IVenueService/DeleteResponse")]
        System.Threading.Tasks.Task<bool> DeleteAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/Get", ReplyAction="http://tempuri.org/IVenueService/GetResponse")]
        DataPresenter.Entity.Venue Get(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/Get", ReplyAction="http://tempuri.org/IVenueService/GetResponse")]
        System.Threading.Tasks.Task<DataPresenter.Entity.Venue> GetAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/GetAll", ReplyAction="http://tempuri.org/IVenueService/GetAllResponse")]
        DataPresenter.Entity.Venue[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/GetAll", ReplyAction="http://tempuri.org/IVenueService/GetAllResponse")]
        System.Threading.Tasks.Task<DataPresenter.Entity.Venue[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/Save", ReplyAction="http://tempuri.org/IVenueService/SaveResponse")]
        int Save(DataPresenter.Entity.Venue venue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/Save", ReplyAction="http://tempuri.org/IVenueService/SaveResponse")]
        System.Threading.Tasks.Task<int> SaveAsync(DataPresenter.Entity.Venue venue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/Update", ReplyAction="http://tempuri.org/IVenueService/UpdateResponse")]
        bool Update(DataPresenter.Entity.Venue venue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IVenueService/Update", ReplyAction="http://tempuri.org/IVenueService/UpdateResponse")]
        System.Threading.Tasks.Task<bool> UpdateAsync(DataPresenter.Entity.Venue venue);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IVenueServiceChannel : TicketManagement.ASP.WCF.VenueService.IVenueService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class VenueServiceClient : System.ServiceModel.ClientBase<TicketManagement.ASP.WCF.VenueService.IVenueService>, TicketManagement.ASP.WCF.VenueService.IVenueService {
        
        public VenueServiceClient() {
        }
        
        public VenueServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public VenueServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VenueServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public VenueServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Delete(int id) {
            return base.Channel.Delete(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAsync(int id) {
            return base.Channel.DeleteAsync(id);
        }
        
        public DataPresenter.Entity.Venue Get(int id) {
            return base.Channel.Get(id);
        }
        
        public System.Threading.Tasks.Task<DataPresenter.Entity.Venue> GetAsync(int id) {
            return base.Channel.GetAsync(id);
        }
        
        public DataPresenter.Entity.Venue[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<DataPresenter.Entity.Venue[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public int Save(DataPresenter.Entity.Venue venue) {
            return base.Channel.Save(venue);
        }
        
        public System.Threading.Tasks.Task<int> SaveAsync(DataPresenter.Entity.Venue venue) {
            return base.Channel.SaveAsync(venue);
        }
        
        public bool Update(DataPresenter.Entity.Venue venue) {
            return base.Channel.Update(venue);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateAsync(DataPresenter.Entity.Venue venue) {
            return base.Channel.UpdateAsync(venue);
        }
    }
}
