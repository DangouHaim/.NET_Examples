using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using BLLIntegratedTests.Properties;
using BLL;
using BLL.ManagerServices;
using DAL.DataEntity;
using Microsoft.EntityFrameworkCore;

namespace BLLIntegratedTests
{
    [TestClass]
    public class VenueManagerIntegratedTests
    {
        VenueService manager { get; set; }
        DataProvider Data { get; set; }
        static TicketManagementContext Context { get; set; }

        [TestInitialize]
        public void Init()
        {
            Context = new TicketManagementContext();
            Data = new DataProvider(Context);
            manager = Data.VenueManager;
            
            Data.Clear();
        }

        [TestMethod]
        public void Save_venue_success_positive_int_returns()
        {
            // arrange
            var v = new Venue()
            {
                Address = "Addr",
                Description = "D",
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
        [ExpectedException(typeof(Exception))]//Повторная вставка уникального 'Description' значения
        //в таблице 'Venue'
        public void Save_venue_failure_negative_int_returns()
        {
            // arrange
            var v = new Venue()
            {
                Address = "Addr",
                Description = "D1",
                Phone = "+111111111111"
            };
            var v2 = new Venue()
            {
                Address = "Addr",
                Description = "D1",
                Phone = "+111111111111"
            };

            // act
            v.Id = manager.Save(v);
            manager.Save(v2);
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
                Description = "D1s",
                Phone = "+111111111111"
            };
            // act
            v.Id = manager.Save(v);
            v = manager.Get(v.Id);
            v.Description = "asdsad1q2312";
            act = manager.Update(v);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Update_venue_failure_false_returns()
        {
            // arrange
            bool act = true;
            bool exp = false;
            var v = new Venue()
            {
                Address = "Addr",
                Description = "D1s",
                Phone = "+111111111111"
            };
            var origin = new Venue()
            {
                Address = "Addr",
                Description = "asd",
                Phone = "+111111111111"
            };
            // act
            manager.Save(v);
            manager.Save(origin);
            v.Description = "asd";
            act = manager.Update(v);//venue with description "asd" already exists

            // assert
            Assert.AreEqual(exp, act);
        }
    }
}
