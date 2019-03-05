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
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "EventSeatService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы EventSeatService.svc или EventSeatService.svc.cs в обозревателе решений и начните отладку.
    public class EventSeatService : IEventSeatService
    {
        private readonly BLL.IServices.IEventSeatService _service;

        public EventSeatService()
        {
            DAL.TicketManagementContext context = new DAL.TicketManagementContext();
            _service = new ManagerServices.EventSeatService(new EntityEventSeatRepository(context));
        }

        public IEnumerable<EventSeat> GetAll()
        {
            return EventSeat.FromEntityList(_service.GetAll());
        }

        public EventSeat Get(int id)
        {
            return EventSeat.FromEntity(_service.Get(id));
        }

        public IEnumerable<EventSeat> GetForEventArea(int eventAreaId)
        {
            return EventSeat.FromEntityList(_service.GetForEventArea(eventAreaId));
        }
    }
}
