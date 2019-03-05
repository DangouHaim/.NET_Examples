

using BLL.ManagerServices;
using DAL;
using DAL.RepositoryBehaviours.Entity;
using Microsoft.EntityFrameworkCore;

namespace BLLIntegratedTests
{
    public class DataProvider
    {
        public VenueService VenueManager { get; set; }
        public LayoutService LayoutManager { get; set; }
        public AreaService AreaManager { get; set; }
        public SeatService SeatManager { get; set; }
        public EventService EventManager { get; set; }
        public EventSeatService EventSeatService { get; set; }
        public EventAreaService EventAreaService { get; set; }
        private TicketManagementContext Context { get; }

        public DataProvider(TicketManagementContext context)
        {
            Context = context;
            VenueManager = new VenueService(new EntityVenueRepository(context));
            LayoutManager = new LayoutService(new EntityLayoutRepository(context));
            AreaManager = new AreaService(new EntityAreaRepository(context));
            SeatManager = new SeatService(new EntitySeatRepository(context));
            EventManager = new EventService(new EntityEventRepository(context), new EntityProcedureManager(context));
            EventSeatService = new EventSeatService(new EntityEventSeatRepository(context));
            EventAreaService = new EventAreaService(new EntityEventAreaRepository(context));
        }

        public void Clear()
        {
            foreach (var v in EventManager.GetAll())
            {
                Context.Entry(v).State = EntityState.Deleted;
            }

            foreach (var v in VenueManager.GetAll())
            {
                Context.Entry(v).State = EntityState.Deleted;
            }

            Context.SaveChanges();
        }
    }
}
