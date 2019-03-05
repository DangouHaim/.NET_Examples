using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System.Collections.Generic;
using BLL;
using BLL.ManagerServices;
using DAL.DataEntity;

namespace BLLUnitTests
{
    [TestClass]
    public class SeatManagerTests
    {
        SeatService manager { get; set; }
        DataProvider Data { get; set; }

        [TestInitialize]
        public void Init()
        {
            Data = new DataProvider();
            manager = Data.SeatManager;

            foreach (var layout in Data.VenueManager.GetAll())
            {
                Data.VenueManager.Delete(layout.Id, Data.EventSeatService, Data.EventAreaService, Data.LayoutManager);
            }
            Data.VenueManager.Save(new Venue() { Id = 13 });

            foreach (var layout in Data.LayoutManager.GetAll())
            {
                Data.LayoutManager.Delete(layout.Id, Data.EventSeatService, Data.EventAreaService);
            }
            Data.LayoutManager.Save(new Layout() { Id = 13, VenueId = 13 }, Data.VenueManager);

            foreach (var layout in Data.AreaManager.GetAll())
            {
                Data.AreaManager.Delete(layout.Id, Data.EventSeatService, Data.EventAreaService);
            }
            Data.AreaManager.Save(new Area() { Id = 13, LayoutId = 13 }, Data.LayoutManager, Data.SeatManager);


        }

        [TestMethod]
        public void Save_seat_success_positive_int_returns()
        {
            // arrange
            var v = new Seat()
            {
                Id = 10,
                Number = 3,
                Row = 3,
                AreaId = 13
            };
            bool act = false;
            bool exp = true;

            // act
            act = manager.Save(v, Data.AreaManager) > -1;

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_seat_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Seat()
            {
                Id = 10,
                AreaId = 13
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
                Id = 11,
                Number = 5,
                Row = 5,
                AreaId = 13
            };
            // act
            v.Id = manager.Save(v, Data.AreaManager);

            var v2 = new Seat()
            {
                Id = 11,
                Number = 6,
                Row = 6,
                AreaId = 13
            };

            act = manager.Update(v2, Data.AreaManager);

            // assert
            Assert.AreEqual(exp, act);
        }
    }
}
