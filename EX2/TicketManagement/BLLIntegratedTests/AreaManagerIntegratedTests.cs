
using System;
using System.Runtime.Remoting.Contexts;
using BLL.ManagerServices;
using DAL;
using DAL.DataEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLLIntegratedTests
{
    [TestClass]
    public class AreaManagerIntegratedTests
    {
        static AreaService Manager { get; set; }
        static DataProvider Data { get; set; }
        static TicketManagementContext Context { get; set; }

        Venue _venue = null;
        Layout _layout = null;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Context = new TicketManagementContext();
            Data = new DataProvider(Context);
            Manager = Data.AreaManager;
        }

        [TestInitialize]
        public void Init()
        {
            Data.Clear();

            _venue = new Venue()
            {
                Description = "venue = new Venue()",
                Address = "venue = new Venue()",
                Phone = "venue = new Venue()"
            };
            _venue.Id = Data.VenueManager.Save(_venue);

            _layout = new Layout()
            {
                Description = "layout = new Layout()",
                VenueId = _venue.Id
            };
            _layout.Id = Data.LayoutManager.Save(_layout, Data.VenueManager);

            var area = new Area()
            {
                Description = "AreaSaveFailureItem",
                CoordX = 1115165,
                CoordY = 1516165,
                LayoutId = _layout.Id
            };

            area.Id = Manager.Save(area, Data.LayoutManager, Data.SeatManager);
        }

        [TestMethod]
        public void Save_area_success_positive_int_returns()
        {
            // arrange

            var v = new Area()
            {
                Description = "Save_area_success_positive_int_returns",
                LayoutId = _layout.Id
            };
            bool act = false;
            bool exp = true;

            // act
            act = Manager.Save(v, Data.LayoutManager, Data.SeatManager) > -1;

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Area with such coords already exists")]
        public void Save_area_failure_negative_int_returns()
        {
            // arrange
            var v = new Area()
            {
                Description = "AreaSaveFailureItem",
                CoordX = 1,
                CoordY = 1,
                LayoutId = _layout.Id
            };


            // act
            v.Id = Manager.Save(v, Data.LayoutManager, Data.SeatManager);
            v.Id = Manager.Save(v, Data.LayoutManager, Data.SeatManager);
        }

        [TestMethod]
        public void Delete_area_success_true_returns()
        {
            // arrange
            bool act = false;
            var v = new Area()
            {
                Description = "Delete_area_success_true_returns",
                LayoutId = _layout.Id,
                CoordX = 112,
                CoordY = 112
            };
            // act
            v.Id = Manager.Save(v, Data.LayoutManager, Data.SeatManager);
            act = Manager.Delete(v.Id, Data.EventSeatService, Data.EventAreaService);

            // assert
            Assert.IsTrue(act);
        }

        [TestMethod]
        public void Delete_area_failure_false_returns()
        {
            // arrange
            int id = -10000;
            bool act = true;
            bool exp = false;
            // act
            act = Manager.Delete(id, Data.EventSeatService, Data.EventAreaService);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Update_area_success_true_returns()
        {
            // arrange
            bool act = false;
            var v = new Area()
            {
                Description = "Update_area_success_true_returns_0098989090",
                LayoutId = _layout.Id
            };
            // act
            v.Id = Manager.Save(v, Data.LayoutManager, Data.SeatManager);
            v.Description = "Update_area_success_true_returns_00234150";

            act = Manager.Update(v, Data.LayoutManager, Data.SeatManager);

            // assert
            Assert.AreEqual(true, act);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Area with such description already exists")]
        public void Update_area_failure_false_returns()
        {
            // arrange
            var v = new Area()
            {
                Description = "Update_area_failure_false_returns_1",
                LayoutId = _layout.Id
            };
            var origin = new Area()
            {
                Description = "Update_area_failure_false_returns_2",
                LayoutId = _layout.Id
            };
            // act
            v.Id = Manager.Save(v, Data.LayoutManager, Data.SeatManager);
            origin.Id = Manager.Save(origin, Data.LayoutManager, Data.SeatManager);
            v.Description = "Update_area_failure_false_returns_2";
            Manager.Update(v, Data.LayoutManager, Data.SeatManager);//venue with description "asd" already exists
        }
    }
}
