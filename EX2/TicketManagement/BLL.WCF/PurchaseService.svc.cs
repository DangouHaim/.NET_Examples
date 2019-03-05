using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BLL.WCF.IServices;
using DataPresenter.Entity;
using DAL.RepositoryBehaviours.Entity;

namespace BLL.WCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "PurchaseService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы PurchaseService.svc или PurchaseService.svc.cs в обозревателе решений и начните отладку.
    public class PurchaseService : IPurchaseService
    {
        private readonly BLL.IServices.IPurchaseService _service;

        public PurchaseService()
        {
            DAL.TicketManagementContext context = new DAL.TicketManagementContext();
            _service = new ManagerServices.PurchaseService(new EntityPurchaseRepository(context));
        }

        public IEnumerable<Purchase> GetAll()
        {
            return Purchase.GetFromEntityList(_service.GetAll());
        }

        public bool Delete(int id)
        {
            return _service.Delete(id);
        }

        public Purchase Get(int id)
        {
            return Purchase.GetFromEntity(_service.Get(id));
        }

        public IEnumerable<Purchase> GetForUser(int userId)
        {
            return Purchase.GetFromEntityList(_service.GetForUser(userId));
        }
    }
}
