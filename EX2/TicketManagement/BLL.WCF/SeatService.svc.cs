using System.Collections.Generic;
using BLL.WCF.IServices;
using DataPresenter.Entity;
using DAL.RepositoryBehaviours.Entity;

namespace BLL.WCF
{
    public class SeatService : ISeatService
    {
        private readonly BLL.IServices.ISeatService _service;
        private readonly BLL.IServices.IEventAreaService _eventAreaService;
        private readonly BLL.IServices.IAreaService _areaService;
        private readonly BLL.IServices.IEventSeatService _eventSeatService;

        public SeatService()
        {
            DAL.TicketManagementContext context = new DAL.TicketManagementContext();
            _service = new ManagerServices.SeatService(new EntitySeatRepository(context));
            _eventAreaService = new ManagerServices.EventAreaService(new EntityEventAreaRepository(context));
            _areaService = new ManagerServices.AreaService(new EntityAreaRepository(context));
            _eventSeatService = new ManagerServices.EventSeatService(new EntityEventSeatRepository(context));
        }

        public bool Delete(int id)
        {
            return _service.Delete(id, _areaService, _eventSeatService, _eventAreaService);
        }

        public Seat Get(int id)
        {
            return Seat.GetFromEntity(_service.Get(id));
        }

        public IEnumerable<Seat> GetAll()
        {
            List<Seat> l = new List<Seat>();
            foreach (var seat in _service.GetAll())
            {
                l.Add(Seat.GetFromEntity(seat));
            }
            return l;
        }

        public int Save(Seat seat)
        {
            try
            {
                return _service.Save(seat.ToEntity(), _areaService);
            }
            catch
            {
                return -1;
            }
        }

        public bool Update(Seat seat)
        {
            try
            {
                return _service.Update(seat.ToEntity(), _areaService);
            }
            catch
            {
                return false;
            }
        }
    }
}
