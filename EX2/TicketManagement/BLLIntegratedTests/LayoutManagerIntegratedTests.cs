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
    public class LayoutManagerIntegratedTests
    {
        LayoutService manager { get; set; }
        DataProvider Data { get; set; }
        static TicketManagementContext Context { get; set; }

        Venue venue = null;

        [TestInitialize]
        public void Init()
        {
            Context = new TicketManagementContext();
            Data = new DataProvider(Context);

            manager = Data.LayoutManager;
            
            Data.Clear();

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
        [ExpectedException(typeof(Exception),
            "Layout with such description already exists")]
        public void Save_layout_failure_negative_int_returns()
        {
            // arrange
            var v = new Layout()
            {
                Description = "D555551",
                VenueId = venue.Id
            };
            var v2 = new Layout()
            {
                Description = "D555551",
                VenueId = venue.Id
            };

            // act
            v.Id = manager.Save(v, Data.VenueManager);
            manager.Save(v2, Data.VenueManager);
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
                VenueId = venue.Id
            };
            // act
            v.Id = manager.Save(v, Data.VenueManager);
            v.Description = "wddwdwcdcdc";
            act = manager.Update(v, Data.VenueManager);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception),
            "Layout with such description already exists")]
        public void Update_layout_failure_false_returns()
        {
            // arrange
            var v = new Layout()
            {
                Description = "D1s",
                VenueId = venue.Id
            };
            var origin = new Layout()
            {
                Description = "asd",
                VenueId = venue.Id
            };
            // act
            v.Id = manager.Save(v, Data.VenueManager);
            origin.Id = manager.Save(origin, Data.VenueManager);
            v.Description = "asd";
            manager.Update(v, Data.VenueManager);//venue with description "asd" already exists
        }
    }
}
