using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLL;
using BLL.ManagerServices;
using DAL.DataEntity;

namespace BLLUnitTests
{
    [TestClass]
    public class VenueManagerTests
    {
        VenueService manager { get; set; }
        DataProvider Data { get; set; }

        [TestInitialize]
        public void Init()
        {
            Data = new DataProvider();
            manager = Data.VenueManager;

            foreach (var v in manager.GetAll())
            {
                manager.Delete(v.Id, Data.EventSeatService, Data.EventAreaService, Data.LayoutManager);
            }
        }

        [TestMethod]
        public void Save_venue_success_positive_int_returns()
        {
            // arrange
            var v = new Venue()
            {
                Address = "Addr",
                Description = "D",
                Id = 10,
                Phone = "+111111111111"
            };
            bool act = false;
            bool exp = true;

            // act
            act = manager.Save(v) > -1;

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_venue_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Venue()
            {
                Address = "Addr",
                Description = "D1s",
                Id = 10,
                Phone = "+111111111111"
            };
            // act
            v.Id = manager.Save(v);
            act = manager.Delete(v.Id, Data.EventSeatService, Data.EventAreaService, Data.LayoutManager);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_venue_failure_false_returns()
        {
            // arrange
            int id = -10000;
            bool act = true;
            bool exp = false;
            // act
            act = manager.Delete(id, Data.EventSeatService, Data.EventAreaService, Data.LayoutManager);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Update_venue_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Venue()
            {
                Address = "Addr",
                Description = "D1sasdasd",
                Id = 10,
                Phone = "+111111111111"
            };
            // act
            v.Id = manager.Save(v);
            var v2 = new Venue()
            {
                Id = v.Id,
                Address = "Addr",
                Description = "avsdvfd165161vdvd",
                Phone = "+111111111111"
            };
            act = manager.Update(v2);

            // assert
            Assert.AreEqual(exp, act);
        }
    }
}
