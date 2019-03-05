using System;
using System.Collections.Generic;
using System.Linq;
using BLL.IServices;
using DAL;
using DAL.DataEntity;
using DAL.IRepository;

namespace BLL.ManagerServices
{
    public class LayoutService : ILayoutService
    {
        private ILayoutRepository Repository { get; }

        public LayoutService(ILayoutRepository repo)
        {
            Repository = repo;
        }

        public bool Delete(int id, IEventSeatService es, IEventAreaService ea)
        {
            var eaAll = ea.GetAll();
            var esAll = es.GetAll();

            var r = from eventArea in eaAll
                join eventSeat in esAll on eventArea.Id equals eventSeat.EventAreaId
                where eventSeat.State != 0
                      && eventArea.LayoutId == id
                select eventArea;
            if (r.Any())
            {
                throw new Exception("Try to delete layout for locked seat");
            }
            return Repository.Delete(id);
        }

        public Layout Get(int id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<Layout> GetAll()
        {
            return Repository.All.ToList();
        }

        public int Save(Layout layout, IVenueService vs)
        {
            var all = GetAll();
            if ((from x in all where x.VenueId == layout.VenueId 
                 && x.Description == layout.Description select x).Any())
            {
                throw new Exception("Layout with such description already exists");
            }
            SaveUpdateValidate(layout, vs);
            return Repository.Save(layout);
        }

        public bool Update(Layout layout, IVenueService vs)
        {
            var all = GetAll();
            if ((from x in all where x.VenueId == layout.VenueId
                 &&x.Description == layout.Description select x).Count() > 1)
            {
                throw new Exception("Layout with such description already exists");
            }
            SaveUpdateValidate(layout, vs);
            return Repository.Update(layout);
        }

        private void SaveUpdateValidate(Layout layout, IVenueService vs)
        {
            var vsAll = vs.GetAll();
            var all = GetAll();
            if (!(from x in vsAll where x.Id == layout.VenueId select x).Any())
            {
                throw new Exception("No such venue");
            }
        }
    }
}
