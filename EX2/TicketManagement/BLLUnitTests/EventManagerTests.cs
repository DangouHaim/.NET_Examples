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
    public class EventManagerTests
    {
        EventService manager { get; set; }
        DataProvider Data { get; set; }

        [TestInitialize]
        public void Init()
        {
            Data = new DataProvider();
            manager = Data.EventManager;
        }

        [TestMethod]
        public void Save_event_success_positive_int_returns()
        {
            // arrange
            var v = new Event()
            {
                Description = "D",
                Id = 10,
                EventDate = DateTime.Now,
                Name = "Save_event_success_positive_int_returns"
            };
            bool act = false;
            bool exp = true;

            // act
            act = manager.Save(v) > -1;

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_event_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;
            var v = new Event()
            {
                Description = "Desc",
                Id = 10,
                EventDate = DateTime.Now,
                Name = "event1"
            };
            // act
            v.Id = manager.Save(v);
            act = manager.Delete(v.Id);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Delete_event_failure_false_returns()
        {
            // arrange
            int id = -10000;
            bool act = true;
            bool exp = false;
            // act
            act = manager.Delete(id);

            // assert
            Assert.AreEqual(exp, act);
        }

        [TestMethod]
        public void Update_event_success_true_returns()
        {
            // arrange
            bool act = false;
            bool exp = true;

            var v = new Event()
            {
                Description = "Update_event_success_true_returns",
                Id = 10,
                EventDate = DateTime.Now,
                Name = "Some name"
            };

            // act
            v.Id = manager.Save(v);
            v.Description = "asdsad";
            act = manager.Update(v);

            // assert
            Assert.AreEqual(exp, act);
        }
    }
}
