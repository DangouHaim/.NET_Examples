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
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "EventAreaService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы EventAreaService.svc или EventAreaService.svc.cs в обозревателе решений и начните отладку.
    public class EventAreaService : IEventAreaService
    {
        private readonly BLL.IServices.IEventAreaService _service;

        public EventAreaService()
        {
            DAL.TicketManagementContext context = new DAL.TicketManagementContext();
            _service = new ManagerServices.EventAreaService(new EntityEventAreaRepository(context));
        }

        public IEnumerable<EventArea> GetAll()
        {
            return EventArea.GetFromEntityList(_service.GetAll());
        }

        public EventArea Get(int id)
        {
            return EventArea.GetFromEntity(_service.Get(id));
        }

        public bool Update(EventArea eventArea)
        {
            return _service.Update(eventArea.ToEntity());
        }

        public IEnumerable<EventArea> GetForEvent(int eventId)
        {
            return EventArea.GetFromEntityList(_service.GetForEvent(eventId));
        }

        public IEnumerable<EventArea> GetForLayout(int layoutId)
        {
            return EventArea.GetFromEntityList(_service.GetForLayout(layoutId));
        }
    }
}
