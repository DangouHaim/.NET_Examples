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
    public class LayoutManagerTests
    {
        LayoutService manager { get; set; }
        DataProvider Data { get; set; }

        Venue venue = null;

        [TestInitialize]
        public void Init()
        {
            Data = new DataProvider();

            manager = Data.LayoutManager;
            foreach (var v in Data.VenueManager.GetAll())
            {
                Data.VenueManager.Delete(v.Id, Data.EventSeatService, Data.EventAreaService, Data.LayoutManager);
            }

            venue = new Venue()
            {
                Address = "addr",
                Description = "asdasdasd",
                Phone = "000"
            };
            venue.Id = Data.VenueManager.Save(venue);
        }

        [TestMethod]
        public void Save_layout_success_positive_int_returns()
        {
            // arrange

            var v = new Layout()
            {
                Description = "D",
                Id = 10,
                VenueId = venue.Id
            };
            bool act = false;
            bool exp = true;

            // act
            act = manager.Save(v, Data.VenueManager) > -1;

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_layout_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Layout()
            {
                Description = "D1saaa",
                Id = 10,
                VenueId = venue.Id
            };
            // act
            v.Id = manager.Save(v, Data.VenueManager);
            act = manager.Delete(v.Id, Data.EventSeatService, Data.EventAreaService);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_layout_failure_false_returns()
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
        public void Update_layout_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;

            var v = new Layout()
            {
                Description = "qczxczxcqqq",
                Id = 10,
                VenueId = venue.Id
            };
            // act
            v.Id = manager.Save(v, Data.VenueManager);

            var v2 = new Layout()
            {
                Description = "qczxcscamnljf45156zxcqqq",
                Id = v.Id,
                VenueId = venue.Id
            };

            act = manager.Update(v2, Data.VenueManager);

            // assert
            Assert.AreEqual(exp, act);
        }
    }
}
