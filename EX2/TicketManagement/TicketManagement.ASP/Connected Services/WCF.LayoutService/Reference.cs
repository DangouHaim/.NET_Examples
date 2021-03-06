//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketManagement.ASP.WCF.LayoutService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCF.LayoutService.ILayoutService")]
    public interface ILayoutService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/Delete", ReplyAction="http://tempuri.org/ILayoutService/DeleteResponse")]
        bool Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/Delete", ReplyAction="http://tempuri.org/ILayoutService/DeleteResponse")]
        System.Threading.Tasks.Task<bool> DeleteAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/Get", ReplyAction="http://tempuri.org/ILayoutService/GetResponse")]
        DataPresenter.Entity.Layout Get(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/Get", ReplyAction="http://tempuri.org/ILayoutService/GetResponse")]
        System.Threading.Tasks.Task<DataPresenter.Entity.Layout> GetAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/GetAll", ReplyAction="http://tempuri.org/ILayoutService/GetAllResponse")]
        DataPresenter.Entity.Layout[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/GetAll", ReplyAction="http://tempuri.org/ILayoutService/GetAllResponse")]
        System.Threading.Tasks.Task<DataPresenter.Entity.Layout[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/Save", ReplyAction="http://tempuri.org/ILayoutService/SaveResponse")]
        int Save(DataPresenter.Entity.Layout layout);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/Save", ReplyAction="http://tempuri.org/ILayoutService/SaveResponse")]
        System.Threading.Tasks.Task<int> SaveAsync(DataPresenter.Entity.Layout layout);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/Update", ReplyAction="http://tempuri.org/ILayoutService/UpdateResponse")]
        bool Update(DataPresenter.Entity.Layout layout);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILayoutService/Update", ReplyAction="http://tempuri.org/ILayoutService/UpdateResponse")]
        System.Threading.Tasks.Task<bool> UpdateAsync(DataPresenter.Entity.Layout layout);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILayoutServiceChannel : TicketManagement.ASP.WCF.LayoutService.ILayoutService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LayoutServiceClient : System.ServiceModel.ClientBase<TicketManagement.ASP.WCF.LayoutService.ILayoutService>, TicketManagement.ASP.WCF.LayoutService.ILayoutService {
        
        public LayoutServiceClient() {
        }
        
        public LayoutServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LayoutServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LayoutServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LayoutServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Delete(int id) {
            return base.Channel.Delete(id);
        }
        
        public System.Threading.Tasks.Task<bool> DeleteAsync(int id) {
            return base.Channel.DeleteAsync(id);
        }
        
        public DataPresenter.Entity.Layout Get(int id) {
            return base.Channel.Get(id);
        }
        
        public System.Threading.Tasks.Task<DataPresenter.Entity.Layout> GetAsync(int id) {
            return base.Channel.GetAsync(id);
        }
        
        public DataPresenter.Entity.Layout[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<DataPresenter.Entity.Layout[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public int Save(DataPresenter.Entity.Layout layout) {
            return base.Channel.Save(layout);
        }
        
        public System.Threading.Tasks.Task<int> SaveAsync(DataPresenter.Entity.Layout layout) {
            return base.Channel.SaveAsync(layout);
        }
        
        public bool Update(DataPresenter.Entity.Layout layout) {
            return base.Channel.Update(layout);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateAsync(DataPresenter.Entity.Layout layout) {
            return base.Channel.UpdateAsync(layout);
        }
    }
}
