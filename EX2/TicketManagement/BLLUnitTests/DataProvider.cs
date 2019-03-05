using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ManagerServices;
using DAL.RepositoryBehaviours.Entity;
using BLLUnitTests.Repository;

namespace BLLUnitTests
{
    public class DataProvider
    {
        public VenueService VenueManager { get; set; }
        public LayoutService LayoutManager { get; set; }
        public AreaService AreaManager { get; set; }
        public SeatService SeatManager { get; set; }
        public EventService EventManager { get; set; }
        public EventAreaService EventAreaService { get; set; }
        public EventSeatService EventSeatService { get; set; }

        public DataProvider()
        {
            VenueManager = new VenueService(new FakeVenueRepo());
            LayoutManager = new LayoutService(new FakeLayoutRepo());
            AreaManager = new AreaService(new FakeAreaRepo());
            SeatManager = new SeatService(new FakeSeatRepo());
            var eventRepo = new FakeEventRepo();
            EventManager = new EventService(eventRepo, new FakePropcedureManager(eventRepo));
            EventAreaService = new EventAreaService(new FakeEventAreaRepo());
            EventSeatService = new EventSeatService(new FakeEventSeatRepo());
        }
    }
}
