//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketManagement.ASP.WCF.SeatService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCF.SeatService.ISeatService")]
    public interface ISeatService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Delete", ReplyAction="http://tempuri.org/ISeatService/DeleteResponse")]
        bool Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Delete", ReplyAction="http://tempuri.org/ISeatService/DeleteResponse")]
        System.Threading.Tasks.Task<bool> DeleteAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Get", ReplyAction="http://tempuri.org/ISeatService/GetResponse")]
        DataPresenter.Entity.Seat Get(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Get", ReplyAction="http://tempuri.org/ISeatService/GetResponse")]
        System.Threading.Tasks.Task<DataPresenter.Entity.Seat> GetAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/GetAll", ReplyAction="http://tempuri.org/ISeatService/GetAllResponse")]
        DataPresenter.Entity.Seat[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/GetAll", ReplyAction="http://tempuri.org/ISeatService/GetAllResponse")]
        System.Threading.Tasks.Task<DataPresenter.Entity.Seat[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Save", ReplyAction="http://tempuri.org/ISeatService/SaveResponse")]
        int Save(DataPresenter.Entity.Seat seat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Save", ReplyAction="http://tempuri.org/ISeatService/SaveResponse")]
        System.Threading.Tasks.Task<int> SaveAsync(DataPresenter.Entity.Seat seat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Update", ReplyAction="http://tempuri.org/ISeatService/UpdateResponse")]
        bool Update(DataPresenter.Entity.Seat seat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Update", ReplyAction="http://tempuri.org/ISeatService/UpdateResponse")]
        System.Threading.Tasks.Task<bool> UpdateAsync(DataPresenter.Entity.Seat seat);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISeatServiceChannel : TicketManagement.ASP.WCF.SeatService.ISeatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SeatServiceClient : System.ServiceModel.ClientBase<TicketManagement.ASP.WCF.SeatService.ISeatService>, TicketManagement.ASP.WCF.SeatService.ISeatService {
        
        public SeatServiceClient() {
        }
        
        public SeatServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SeatServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SeatServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SeatServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Delete(int id) {
            return base.Channel.Delete(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAsync(int id) {
            return base.Channel.DeleteAsync(id);
        }
        
        public DataPresenter.Entity.Seat Get(int id) {
            return base.Channel.Get(id);
        }
        
        public System.Threading.Tasks.Task<DataPresenter.Entity.Seat> GetAsync(int id) {
            return base.Channel.GetAsync(id);
        }
        
        public DataPresenter.Entity.Seat[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<DataPresenter.Entity.Seat[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public int Save(DataPresenter.Entity.Seat seat) {
            return base.Channel.Save(seat);
        }
        
        public System.Threading.Tasks.Task<int> SaveAsync(DataPresenter.Entity.Seat seat) {
            return base.Channel.SaveAsync(seat);
        }
        
        public bool Update(DataPresenter.Entity.Seat seat) {
            return base.Channel.Update(seat);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateAsync(DataPresenter.Entity.Seat seat) {
            return base.Channel.UpdateAsync(seat);
        }
    }
}
