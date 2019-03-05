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
    public class AreaManagerTests
    {
        AreaService manager { get; set; }

        DataProvider Data { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
        }

        [TestInitialize]
        public void Init()
        {
            Data = new DataProvider();
            manager = Data.AreaManager;

            foreach (var layout in Data.VenueManager.GetAll())
            {
                Data.VenueManager.Delete(layout.Id, Data.EventSeatService, Data.EventAreaService, Data.LayoutManager);
            }
            Data.VenueManager.Save(new Venue() { Id = 13 });

            foreach (var layout in Data.LayoutManager.GetAll())
            {
                Data.LayoutManager.Delete(layout.Id, Data.EventSeatService, Data.EventAreaService);
            }
            Data.LayoutManager.Save(new Layout() { Id = 13, VenueId = 13}, Data.VenueManager);
        }

        [TestMethod]
        public void Save_area_success_positive_int_returns()
        {
            // arrange

            var v = new Area()
            {
                Description = "Save_area_success_positive_int_returns",
                Id = 10,
                LayoutId = 13
            };
            bool act = false;
            bool exp = true;

            // act
            act = manager.Save(v, Data.LayoutManager, Data.SeatManager) > -1;

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_area_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Area()
            {
                Description = "Delete_area_success_true_returns",
                CoordX = 112,
                CoordY = 112,
                LayoutId = 13
            };

            // act
            v.Id = manager.Save(v, Data.LayoutManager, Data.SeatManager);
            act = manager.Delete(v.Id, Data.EventSeatService, Data.EventAreaService);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_area_failure_false_returns()
        {
            // arrange
            int id = -10000;
            bool act = true;
            bool exp = false;
            // act
            act = manager.Delete(id, Data.EventSeatService, Data.EventAreaService);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Update_area_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Area()
            {
                Description = "Update_area_success_true_returns",
                Id = 10,
                LayoutId = 13,
                CoordX = 1651,
                CoordY = 16181651
            };
            // act
            v.Id = manager.Save(v, Data.LayoutManager, Data.SeatManager);

            var v2 = new Area()
            {
                Description = "Update_area_success_true_returns_asdsad",
                Id = 10,
                LayoutId = 13,
                CoordX = 1651,
                CoordY = 16181651
            };

            act = manager.Update(v2, Data.LayoutManager, Data.SeatManager);

            // assert
            Assert.AreEqual(exp, act);
        }
    }
}
