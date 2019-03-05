using System.Collections.Generic;
using BLL.ManagerServices;
using BLL.WCF.IServices;
using DataPresenter.Entity;
using DAL.RepositoryBehaviours.Entity;

namespace BLL.WCF
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "VenueService" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы VenueService.svc или VenueService.svc.cs в обозревателе решений и начните отладку.
    
    public class VenueService : IVenueService
    {

        private readonly BLL.IServices.IVenueService _service;
        private readonly BLL.IServices.IEventSeatService _eventSeatService;
        private readonly BLL.IServices.ILayoutService _layoutService;
        private readonly BLL.IServices.IEventAreaService _eventAreaService;

        public VenueService()
        {
            DAL.TicketManagementContext context = new DAL.TicketManagementContext();
            DAL.TicketManagementContext depContext = new DAL.TicketManagementContext();
            _service = new ManagerServices.VenueService(new EntityVenueRepository(context));
            _eventSeatService = new ManagerServices.EventSeatService(new EntityEventSeatRepository(new DAL.TicketManagementContext()));
            _layoutService = new ManagerServices.LayoutService(new EntityLayoutRepository(new DAL.TicketManagementContext()));
            _eventAreaService = new ManagerServices.EventAreaService(new EntityEventAreaRepository(new DAL.TicketManagementContext()));;
        }

        public bool Delete(int id)
        {
            return _service.Delete(id, _eventSeatService, _eventAreaService, _layoutService);
        }

        public Venue Get(int id)
        {
            return Venue.FromEntity(_service.Get(id));
        }

        public IEnumerable<Venue> GetAll()
        {
            List<Venue> l = new List<Venue>();
            foreach (var venue in _service.GetAll())
            {
                l.Add(Venue.FromEntity(venue));
            }
            return l;
        }

        public int Save(Venue venue)
        {
            try
            {
                return _service.Save(venue.ToEntity());
            }
            catch
            {
                return -1;
            }
        }

        public bool Update(Venue venue)
        {
            try
            {
                return _service.Update(venue.ToEntity());
            }
            catch
            {
                return false;
            }
        }
    }
}
