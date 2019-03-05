﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketManagement.WPF.WCF.SeatService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Seat", Namespace="http://schemas.datacontract.org/2004/07/DataPresenter.Entity")]
    [System.SerializableAttribute()]
    public partial class Seat : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AreaIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RowField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int AreaId {
            get {
                return this.AreaIdField;
            }
            set {
                if ((this.AreaIdField.Equals(value) != true)) {
                    this.AreaIdField = value;
                    this.RaisePropertyChanged("AreaId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Number {
            get {
                return this.NumberField;
            }
            set {
                if ((this.NumberField.Equals(value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Row {
            get {
                return this.RowField;
            }
            set {
                if ((this.RowField.Equals(value) != true)) {
                    this.RowField = value;
                    this.RaisePropertyChanged("Row");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCF.SeatService.ISeatService")]
    public interface ISeatService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Delete", ReplyAction="http://tempuri.org/ISeatService/DeleteResponse")]
        bool Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Delete", ReplyAction="http://tempuri.org/ISeatService/DeleteResponse")]
        System.Threading.Tasks.Task<bool> DeleteAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Get", ReplyAction="http://tempuri.org/ISeatService/GetResponse")]
        TicketManagement.WPF.WCF.SeatService.Seat Get(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Get", ReplyAction="http://tempuri.org/ISeatService/GetResponse")]
        System.Threading.Tasks.Task<TicketManagement.WPF.WCF.SeatService.Seat> GetAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/GetAll", ReplyAction="http://tempuri.org/ISeatService/GetAllResponse")]
        TicketManagement.WPF.WCF.SeatService.Seat[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/GetAll", ReplyAction="http://tempuri.org/ISeatService/GetAllResponse")]
        System.Threading.Tasks.Task<TicketManagement.WPF.WCF.SeatService.Seat[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Save", ReplyAction="http://tempuri.org/ISeatService/SaveResponse")]
        int Save(TicketManagement.WPF.WCF.SeatService.Seat seat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Save", ReplyAction="http://tempuri.org/ISeatService/SaveResponse")]
        System.Threading.Tasks.Task<int> SaveAsync(TicketManagement.WPF.WCF.SeatService.Seat seat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Update", ReplyAction="http://tempuri.org/ISeatService/UpdateResponse")]
        bool Update(TicketManagement.WPF.WCF.SeatService.Seat seat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatService/Update", ReplyAction="http://tempuri.org/ISeatService/UpdateResponse")]
        System.Threading.Tasks.Task<bool> UpdateAsync(TicketManagement.WPF.WCF.SeatService.Seat seat);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISeatServiceChannel : TicketManagement.WPF.WCF.SeatService.ISeatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SeatServiceClient : System.ServiceModel.ClientBase<TicketManagement.WPF.WCF.SeatService.ISeatService>, TicketManagement.WPF.WCF.SeatService.ISeatService {
        
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
        
        public TicketManagement.WPF.WCF.SeatService.Seat Get(int id) {
            return base.Channel.Get(id);
        }
        
        public System.Threading.Tasks.Task<TicketManagement.WPF.WCF.SeatService.Seat> GetAsync(int id) {
            return base.Channel.GetAsync(id);
        }
        
        public TicketManagement.WPF.WCF.SeatService.Seat[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<TicketManagement.WPF.WCF.SeatService.Seat[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public int Save(TicketManagement.WPF.WCF.SeatService.Seat seat) {
            return base.Channel.Save(seat);
        }
        
        public System.Threading.Tasks.Task<int> SaveAsync(TicketManagement.WPF.WCF.SeatService.Seat seat) {
            return base.Channel.SaveAsync(seat);
        }
        
        public bool Update(TicketManagement.WPF.WCF.SeatService.Seat seat) {
            return base.Channel.Update(seat);
        }
        
        public System.Threading.Tasks.Task<bool> UpdateAsync(TicketManagement.WPF.WCF.SeatService.Seat seat) {
            return base.Channel.UpdateAsync(seat);
        }
    }
}
