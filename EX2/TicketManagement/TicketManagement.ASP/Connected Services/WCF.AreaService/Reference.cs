//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketManagement.ASP.WCF.AreaService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCF.AreaService.IAreaService")]
    public interface IAreaService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/Delete", ReplyAction="http://tempuri.org/IAreaService/DeleteResponse")]
        bool Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/Delete", ReplyAction="http://tempuri.org/IAreaService/DeleteResponse")]
        System.Threading.Tasks.Task<bool> DeleteAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/Get", ReplyAction="http://tempuri.org/IAreaService/GetResponse")]
        DataPresenter.Entity.Area Get(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/Get", ReplyAction="http://tempuri.org/IAreaService/GetResponse")]
        System.Threading.Tasks.Task<DataPresenter.Entity.Area> GetAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/GetAll", ReplyAction="http://tempuri.org/IAreaService/GetAllResponse")]
        DataPresenter.Entity.Area[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/GetAll", ReplyAction="http://tempuri.org/IAreaService/GetAllResponse")]
        System.Threading.Tasks.Task<DataPresenter.Entity.Area[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/Save", ReplyAction="http://tempuri.org/IAreaService/SaveResponse")]
        int Save(DataPresenter.Entity.Area area);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/Save", ReplyAction="http://tempuri.org/IAreaService/SaveResponse")]
        System.Threading.Tasks.Task<int> SaveAsync(DataPresenter.Entity.Area area);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/Update", ReplyAction="http://tempuri.org/IAreaService/UpdateResponse")]
        bool Update(DataPresenter.Entity.Area area);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAreaService/Update", ReplyAction="http://tempuri.org/IAreaService/UpdateResponse")]
        System.Threading.Tasks.Task<bool> UpdateAsync(DataPresenter.Entity.Area area);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAreaServiceChannel : TicketManagement.ASP.WCF.AreaService.IAreaService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AreaServiceClient : System.ServiceModel.ClientBase<TicketManagement.ASP.WCF.AreaService.IAreaService>, TicketManagement.ASP.WCF.AreaService.IAreaService {
        
        public AreaServiceClient() {
        }
        
        public AreaServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AreaServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AreaServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AreaServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Delete(int id) {
            return base.Channel.Delete(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAsync(int id) {
            return base.Channel.DeleteAsync(id);
        }
        
        public DataPresenter.Entity.Area Get(int id) {
            return base.Channel.Get(id);
        }
        
        public System.Threading.Tasks.Task<DataPresenter.Entity.Area> GetAsync(int id) {
            return base.Channel.GetAsync(id);
        }
        
        public DataPresenter.Entity.Area[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<DataPresenter.Entity.Area[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public int Save(DataPresenter.Entity.Area area) {
            return base.Channel.Save(area);
        }
        
        public System.Threading.Tasks.Task<int> SaveAsync(DataPresenter.Entity.Area area) {
            return base.Channel.SaveAsync(area);
        }
        
        public bool Update(DataPresenter.Entity.Area area) {
            return base.Channel.Update(area);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateAsync(DataPresenter.Entity.Area area) {
            return base.Channel.UpdateAsync(area);
        }
    }
}
