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
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "LayoutService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы LayoutService.svc или LayoutService.svc.cs в обозревателе решений и начните отладку.
    public class LayoutService : ILayoutService
    {
        private readonly BLL.IServices.ILayoutService _service;
        private readonly BLL.IServices.IEventAreaService _eventAreaService;
        private readonly BLL.IServices.IEventSeatService _eventSeatService;
        private readonly BLL.IServices.IVenueService _venueService;

        public LayoutService()
        {
            DAL.TicketManagementContext context = new DAL.TicketManagementContext();
            _service = new ManagerServices.LayoutService(new EntityLayoutRepository(context));
            _eventAreaService = new ManagerServices.EventAreaService(new EntityEventAreaRepository(context));
            _eventSeatService = new ManagerServices.EventSeatService(new EntityEventSeatRepository(context));
            _venueService = new ManagerServices.VenueService(new EntityVenueRepository(context));
        }

        public bool Delete(int id)
        {
            return _service.Delete(id, _eventSeatService, _eventAreaService);
        }

        public Layout Get(int id)
        {
            return Layout.FromEntity(_service.Get(id));
        }

        public IEnumerable<Layout> GetAll()
        {
            List<Layout> l = new List<Layout>();
            foreach (var v in _service.GetAll())
            {
                l.Add(Layout.FromEntity(v));
            }
            return l;
        }

        public int Save(Layout layout)
        {
            try
            {
                return _service.Save(layout.ToEntity(), _venueService);
            }
            catch
            {
                return -1;
            }
        }

        public bool Update(Layout layout)
        {
            return _service.Update(layout.ToEntity(), _venueService);
        }
    }
}
