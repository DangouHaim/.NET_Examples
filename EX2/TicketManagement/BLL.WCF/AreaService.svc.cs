using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BLL.ManagerServices;
using BLL.WCF.IServices;
using DataPresenter.Entity;
using DAL.RepositoryBehaviours.Entity;

namespace BLL.WCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "AreaService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы AreaService.svc или AreaService.svc.cs в обозревателе решений и начните отладку.
    public class AreaService : IAreaService
    {
        private readonly BLL.IServices.IAreaService _service;
        private readonly BLL.IServices.IEventSeatService _eventSeatService;
        private readonly BLL.IServices.IEventAreaService _eventAreaService;
        private readonly BLL.IServices.ILayoutService _layoutService;
        private readonly BLL.IServices.ISeatService _seatService;

        public AreaService()
        {
            DAL.TicketManagementContext context = new DAL.TicketManagementContext();
            _service = new ManagerServices.AreaService(new EntityAreaRepository(context));
            _eventSeatService = new ManagerServices.EventSeatService(new EntityEventSeatRepository(context));
            _eventAreaService = new ManagerServices.EventAreaService(new EntityEventAreaRepository(context));
            _layoutService = new ManagerServices.LayoutService(new EntityLayoutRepository(context));
            _seatService = new ManagerServices.SeatService(new EntitySeatRepository(context));
        }

        public bool Delete(int id)
        {
            return _service.Delete(id, _eventSeatService, _eventAreaService);
        }

        public Area Get(int id)
        {
            return Area.FromEntity(_service.Get(id));
        }

        public IEnumerable<Area> GetAll()
        {
            List<Area> l = new List<Area>();
            foreach (var area in _service.GetAll())
            {
                l.Add(Area.FromEntity(area));
            }
            return l;
        }

        public int Save(Area area)
        {
            try
            {
                return _service.Save(area.ToEntity(), _layoutService, _seatService);
            }
            catch
            {
                return -1;
            }
        }

        public bool Update(Area area)
        {
            try
            {
                return _service.Update(area.ToEntity(), _layoutService, _seatService);
            }
            catch
            {
                return false;
            }
        }
    }
}
