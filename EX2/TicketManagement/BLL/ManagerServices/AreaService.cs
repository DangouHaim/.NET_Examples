using System;
using System.Collections.Generic;
using System.Linq;
using BLL.IServices;
using DAL;
using DAL.IRepository;

namespace BLL.ManagerServices
{
    public class AreaService : IAreaService
    {
        private IAreaRepository Repository { get; }

        public AreaService(IAreaRepository repo)
        {
            Repository = repo;
        }

        public bool Delete(int id, IEventSeatService es, IEventAreaService ea)
        {
            var all = GetAll();
            var eaAll = ea.GetAll();
            var esAll = es.GetAll();

            var r = from area in all
                join eventArea in eaAll on area.LayoutId equals eventArea.LayoutId
                join eventSeat in esAll on eventArea.Id equals eventSeat.EventAreaId
                where eventSeat.State != 0
                      && area.Id == id select area;
            if (r.Any())
            {
                throw new Exception("Try to delete area for locked seat");
            }
            return Repository.Delete(id);
        }

        public Area Get(int id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<Area> GetAll()
        {
            return Repository.All.ToList();
        }

        public int Save(Area area, ILayoutService ls, ISeatService ss)
        {
            var all = GetAll();
            if ((from x in all
                 where x.LayoutId == area.LayoutId
                     && x.Description == area.Description
                 select x).Any())
            {
                throw new Exception("Area with such description already exists");
            }

            if ((from x in all
                 where x.LayoutId == area.LayoutId
                     && (x.CoordX == area.CoordX || x.CoordY == area.CoordY)
                 select x).Any())
            {
                throw new Exception("Area with such coords already exists");
            }
            SaveUpdateValidate(area, ls, ss);
            return Repository.Save(area);
        }

        public bool Update(Area area, ILayoutService ls, ISeatService ss)
        {
            var all = GetAll();
            if (Get(area.Id).Description != area.Description
                || Get(area.Id).LayoutId != area.LayoutId)
            {
                if ((from x in all
                     where x.LayoutId == area.LayoutId
                         && x.Description == area.Description
                     select x).Any())
                {
                    throw new Exception("Area with such description already exists");
                }
            }

            if (Get(area.Id).CoordX != area.CoordX
                || Get(area.Id).LayoutId != area.LayoutId)
            {
                if ((from x in all
                     where x.LayoutId == area.LayoutId
                         && (x.CoordX == area.CoordX)
                     select x).Any())
                {
                    throw new Exception("Area with such coords already exists");
                }
            }

            if (Get(area.Id).CoordY != area.CoordY
                || Get(area.Id).LayoutId != area.LayoutId)
            {
                if ((from x in all
                     where x.LayoutId == area.LayoutId
                         && (x.CoordY == area.CoordY)
                     select x).Any())
                {
                    throw new Exception("Area with such coords already exists");
                }
            }

            SaveUpdateValidate(area, ls, ss);
            return Repository.Update(area);
        }

        private void SaveUpdateValidate(Area area, ILayoutService ls, ISeatService ss)
        {
            var lsAll = ls.GetAll();
            var ssAll = ss.GetAll();
            var all = GetAll();

            if (!(from x in lsAll
                where x.Id == area.LayoutId
                select x).Any())
            {
                throw new Exception("No such layout");
            }

            var areaLowerSet = from x in all
                               where x.LayoutId == area.LayoutId
                               && (x.CoordX < area.CoordX
                               || x.CoordY < area.CoordY)
                               select x;

            if(areaLowerSet.Any())
            {
                foreach(var v in areaLowerSet.ToList())
                {
                    var seatSet = from x in ssAll where x.AreaId == v.Id select x;
                    if(seatSet.Any())
                    {
                        if(v.CoordX + seatSet.Max(x => x.Row) > area.CoordX
                            || v.CoordY + seatSet.Max(x => x.Number) > area.CoordY)
                        {
                            throw new Exception("Area coords out of range");
                        }
                    }
                }
            }
        }
    }
}
