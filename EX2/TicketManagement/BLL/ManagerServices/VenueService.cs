using System;
using System.Collections.Generic;
using System.Linq;
using BLL.IServices;
using DAL;
using DAL.IRepository;

namespace BLL.ManagerServices
{
    public class VenueService : IVenueService
    {
        private IVenueRepository Repository { get; }

        public VenueService(IVenueRepository repo)
        {
            Repository = repo;
        }

        public bool Delete(int id, IEventSeatService ess, IEventAreaService eas, ILayoutService ls)
        {
            var all = GetAll();
            var esAll = ess.GetAll();
            var eaAll = eas.GetAll();
            var lsAll = ls.GetAll();

            if ((from venue in all
                    join layout in lsAll on venue.Id equals layout.VenueId
                    join eventArea in eaAll on layout.Id equals eventArea.LayoutId
                    join eventSeat in esAll on eventArea.Id equals eventSeat.EventAreaId
                    where eventSeat.State != 0
                    select venue
                ).Any())
            {
                throw new Exception("Try to delete venue with locked seats");
            }

            return Repository.Delete(id);
        }

        public Venue Get(int id)
        {
            return Repository.Get(id);
        }

        public IEnumerable<Venue> GetAll()
        {
            return Repository.All.ToList();
        }

        public int Save(Venue venue)
        {
            SaveUpdateValidate(venue);
            return Repository.Save(venue);
        }

        public bool Update(Venue venue)
        {
            var all = GetAll();

            if ((from x in all
                 where x.Description == venue.Description
                 select x).Count() > 1)
            {
                throw new Exception("Venue description '" + venue.Description + "' is not unique");
            }
            return Repository.Update(venue);
        }

        private void SaveUpdateValidate(Venue venue)
        {
            var all = GetAll();

            if ((from x in all
                where x.Description == venue.Description
                select x).Any())
            {
                throw new Exception("Venue description '" + venue.Description + "' is not unique");
            }
        }
    }
}
