using System;
using System.Collections.Generic;
using System.Linq;
using BLL.IServices;
using DAL;
using DAL.DataEntity;
using DAL.IRepository;

namespace BLL.ManagerServices
{
    public class SeatService : ISeatService
    {
        private ISeatRepository Repository { get; }

        public SeatService(ISeatRepository repo)
        {
            Repository = repo;
        }

        public bool Delete(int id, IAreaService a, IEventSeatService es, IEventAreaService ea)
        {
            var all = GetAll();
            var aAll = a.GetAll();
            var eaAll = ea.GetAll();
            var esAll = es.GetAll();

            var r = from seat in  all
                join area in aAll on seat.AreaId equals area.Id
                join eventArea in eaAll on area.LayoutId equals eventArea.LayoutId
                join eventSeat in esAll on eventArea.Id equals eventSeat.EventAreaId
                where eventSeat.State != 0
                      && seat.Id == id
                select seat;
            if (r.Any())
            {
                throw new Exception("Try to delete locked seat");
            }
            return Repository.Delete(id);
        }

        public Seat Get(int id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<Seat> GetAll()
        {
            return Repository.All.ToList();
        }

        public int Save(Seat seat, IAreaService @as)
        {
            var all = GetAll();

            if (((from x in GetAll()
                  where x.AreaId == seat.AreaId
                        && (seat.Row == x.Row || seat.Number == x.Number)
                  select x).Any()))
            {
                throw new Exception("Seat with such coords already exists");
            }
            SaveUpdateValidate(seat, @as);
            return Repository.Save(seat);
        }

        public bool Update(Seat seat, IAreaService @as)
        {
            var all = GetAll();
            if (Get(seat.Id).Row != seat.Row || Get(seat.Id).AreaId != seat.AreaId)
            {
                if (((from x in GetAll()
                      where x.AreaId == seat.AreaId
                            && (seat.Row == x.Row)
                      select x).Any()))
                {
                    throw new Exception("Seat with such coords already exists");
                }
            }
            if (Get(seat.Id).Number != seat.Number || Get(seat.Id).AreaId != seat.AreaId)
            {
                if (((from x in GetAll()
                      where x.AreaId == seat.AreaId
                            && (seat.Number == x.Number)
                      select x).Any()))
                {
                    throw new Exception("Seat with such coords already exists");
                }
            }
            SaveUpdateValidate(seat, @as);
            return Repository.Update(seat);
        }

        private void SaveUpdateValidate(Seat seat, IAreaService @as)
        {
            var asAll = @as.GetAll();
            var all = GetAll();

            if (!(from x in asAll where x.Id == seat.AreaId select x).Any())
            {
                throw new Exception("No such area");
            }
            var r = from x in asAll where x.Id == seat.AreaId select x;
            if(r.Any())
            {
                int coordX = @as.Get(seat.AreaId).CoordX;
                int coordY = @as.Get(seat.AreaId).CoordY;
                int lid = r.First().LayoutId;
                if ((from x in asAll
                     where x.Id != seat.AreaId && x.LayoutId == lid && x.CoordX > coordX && x.CoordY > coordY
                           && (coordX + seat.Row >= x.CoordX || coordY + seat.Number >= x.CoordY)
                     select x).Any())
                {
                    throw new Exception("Seat coords out of range");
                }
            }
        }
    }
}
