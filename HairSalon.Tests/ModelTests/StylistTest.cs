using System;
using MySql.Data.MySqlClient;
using HairSalon.Models;
using HairSalon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTest : DBTest, IDisposable
    {

        public void Dispose()
        {
            DB.Clear();
        }

        [TestMethod]
        public void Stylist_GetAllInitiallyEmpty
        {
            Assert.AreEqual(0, Stylist.GetAll().Length);
        }
    }
}
