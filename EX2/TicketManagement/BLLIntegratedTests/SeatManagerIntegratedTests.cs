using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System.Collections.Generic;
using BLLIntegratedTests.Properties;
using BLL;
using BLL.ManagerServices;
using DAL.DataEntity;
using Microsoft.EntityFrameworkCore;

namespace BLLIntegratedTests
{
    [TestClass]
    public class SeatManagerIntegratedTests
    {
        SeatService manager { get; set; }
        DataProvider Data { get; set; }
        static TicketManagementContext Context { get; set; }

        Venue venue = null;
        Layout layout = null;
        Area area = null;

        [TestInitialize]
        public void Init()
        {
            Context = new TicketManagementContext();
            Data = new DataProvider(Context);

            manager = Data.SeatManager;

            Data.Clear();

            venue = new Venue()
            {
                Description = "asdasd",
                Address = "asdas",
                Phone = "asdasd"
            };
            venue.Id = Data.VenueManager.Save(venue);

            layout = new Layout()
            {
                Description = "asdasd",
                VenueId = venue.Id
            };
            layout.Id = Data.LayoutManager.Save(layout, Data.VenueManager);

            area = new Area()
            {
                Description = "asdasdas",
                LayoutId = layout.Id,
                CoordX = 10,
                CoordY = 10,
            };
            area.Id = Data.AreaManager.Save(area, Data.LayoutManager, Data.SeatManager);
        }

        [TestMethod]
        public void Save_seat_success_positive_int_returns()
        {
            // arrange
            var v = new Seat()
            {
                Number = 3,
                Row = 3,
                AreaId = area.Id
            };
            bool act = false;
            bool exp = true;

            // act
            act = manager.Save(v, Data.AreaManager) > -1;

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Seat with such coords already exists")]
        public void Save_seat_failure_negative_int_returns()
        {
            // arrange
            var v = new Seat()
            {
                Number = 3,
                Row = 3,
                AreaId = area.Id
            };
            var v2 = new Seat()
            {
                Number = 3,
                Row = 3,
                AreaId = area.Id
            };

            // act
            manager.Save(v, Data.AreaManager);
            manager.Save(v2, Data.AreaManager);
        }

        [TestMethod]
        public void Delete_seat_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Seat()
            {
                AreaId = area.Id
            };
            // act
            v.Id = manager.Save(v, Data.AreaManager);
            act = manager.Delete(v.Id, Data.AreaManager, Data.EventSeatService, Data.EventAreaService);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_seat_failure_false_returns()
        {
            // arrange
            int id = -10000;
            bool act = true;
            bool exp = false;
            // act
            act = manager.Delete(id, Data.AreaManager, Data.EventSeatService, Data.EventAreaService);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Update_seat_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Seat()
            {
                Number = 500,
                Row = 500,
                AreaId = area.Id
            };
            // act
            v.Id = manager.Save(v, Data.AreaManager);
            v.Row = 600;
            v.Number = 600;
            act = manager.Update(v, Data.AreaManager);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Seat with such coords already exists")]
        public void Update_seat_failure_false_returns()
        {
            // arrange
            var v = new Seat()
            {
                Row = 7,
                Number = 7,
                AreaId = area.Id
            };
            var origin = new Seat()
            {
                Row = 8,
                Number = 8,
                AreaId = area.Id
            };
            // act

            v.Id = manager.Save(v, Data.AreaManager);
            origin.Id = manager.Save(origin, Data.AreaManager);
            v.Row = 8;
            v.Number = 8;
            manager.Update(v, Data.AreaManager);
        }
    }
}
