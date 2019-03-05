
using System;
using System.Linq;
using BLL.ManagerServices;
using DAL;
using DAL.DataEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLLIntegratedTests
{
    [TestClass]
    public class EventManagerIntegratedTests
    {
        EventService manager { get; set; }
        DataProvider Data { get; set; }
        static TicketManagementContext Context { get; set; }

        Layout layout = null;
        Layout layoutWithoutSeats = null;

        [TestInitialize]
        public void Init()
        {
            Context = new TicketManagementContext();
            Data = new DataProvider(Context);
            manager = Data.EventManager;

            Data.Clear();

            var venue = new Venue()
            {
                Address = "sadasd",
                Description = "asdsadas",
                Phone = "000",
            };
            venue.Id = Data.VenueManager.Save(venue);

            layout = new Layout()
            {
                Description = "asdadas",
                VenueId = venue.Id,
            };
            layout.Id = Data.LayoutManager.Save(layout, Data.VenueManager);

            var area = new Area()
            {
                CoordX = 10,
                CoordY = 20,
                Description = "asdasd",
                LayoutId = layout.Id,
            };
            area.Id = Data.AreaManager.Save(area, Data.LayoutManager, Data.SeatManager);

            var seat = new Seat()
            {
                AreaId = area.Id,
                Number = 3,
                Row = 3
            };
            seat.Id = Data.SeatManager.Save(seat, Data.AreaManager);

            var venueWS = new Venue()
            {
                Address = "venueWS",
                Description = "venueWS",
                Phone = "000",
            };
            venueWS.Id = Data.VenueManager.Save(venueWS);

            layoutWithoutSeats = new Layout()
            {
                Description = "layoutWS",
                VenueId = venueWS.Id,
            };
            layoutWithoutSeats.Id = Data.LayoutManager.Save(layoutWithoutSeats, Data.VenueManager);

            var areaWS = new Area()
            {
                CoordX = 12,
                CoordY = 15,
                Description = "areaWS",
                LayoutId = layoutWithoutSeats.Id,
            };
            areaWS.Id = Data.AreaManager.Save(areaWS, Data.LayoutManager, Data.SeatManager);
        }

        [TestMethod]
        public void Save_event_success_positive_int_returns()
        {
            // arrange
            var v = new Event()
            {
                Description = "D",
                Id = 10,
                LayoutId = layout.Id,
                EventDate = DateTime.Now,
                Name = "Save_event_success_positive_int_returns"
            };
            bool act = false;
            bool exp = true;

            // act
            int r = manager.Save(v);
            
            act = r > -1;

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Save_event_failure_negative_int_returns()
        {
            // arrange
            var v = new Event()
            {
                Description = "D1",
                LayoutId = layoutWithoutSeats.Id,
                EventDate = DateTime.Now,
                Name = "Save_event_failure_negative_int_returns_1"
            };
            bool act;

            manager.Save(v);

            var v2 = new Event()
            {
                Description = "D1",
                LayoutId = layoutWithoutSeats.Id,
                EventDate = DateTime.Now,
                Name = "Save_event_failure_negative_int_returns_1"
            };

            // act
            act = manager.Save(v2) <= 0;

            // assert
            Assert.IsTrue(act);
        }

        [TestMethod]
        public void Delete_event_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Event()
            {
                Description = "Descasdasd",
                Id = 10,
                LayoutId = layout.Id,
                EventDate = DateTime.Now,
                Name = "event1asdasd"
            };
            // act
            v.Id = manager.Save(v);
            
            act = manager.Delete(v.Id);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_event_failure_false_returns()
        {
            // arrange
            int id = manager.GetForLayout(layout.Id).First().Id;
            // act
            manager.Delete(id);
        }

        [TestMethod]
        public void Update_event_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;

            var v = new Event()
            {
                Description = "Update_event_success_true_returns_0120991",
                EventDate = DateTime.Now,
                LayoutId = layout.Id,
                Name = "Some name ascas"
            };

            // act
            v.Id = manager.Save(v);
            v.Name = "Update_event_success_true_returns_asdsad_213541602";
            act = manager.Update(v);

            // assert
            Assert.AreEqual(exp, act);
        }
    }
}
