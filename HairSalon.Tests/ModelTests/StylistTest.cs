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
            Stylist.DeleteAll();
        }

        [TestMethod]
        public void Stylist_GetAllInitiallyEmpty()
        {
            Assert.AreEqual(0, Stylist.GetAll().Length);
        }

        [TestMethod]
        public void Stylist_SavesToDatabase()
        {
            Stylist newGuy = new Stylist("Dave");

            newGuy.Save();

            Assert.AreEqual(1, Stylist.GetAll().Length);
            Assert.AreNotEqual(-1, newGuy.GetID());
        }

        [TestMethod]
        public void Stylist_Save_DoesNothingWhenSavingTwice()
        {
            Stylist doubleGuy = new Stylist("Harry Houdini");

            doubleGuy.Save();
            doubleGuy.Save();

            Assert.AreEqual(1, Stylist.GetAll().Length);
        }

        [TestMethod]
        public void Stylist_GetByID_ReturnsCorrectStylist()
        {
            Stylist stylist1 = new Stylist("Monica");
            Stylist stylist2 = new Stylist("Phil");

            stylist1.Save();
            stylist2.Save();

            Stylist s1FromDB = Stylist.GetByID(stylist1.GetID());
            Assert.AreEqual(stylist1.GetName(), s1FromDB.GetName());
        }

        [TestMethod]
        public void Stylist_GetByName_ReturnsCorrectResult()
        {
            Stylist newStylist = new Stylist("Monica");

            newStylist.Save();

            Assert.AreEqual(newStylist.GetID(), Stylist.GetByName("Monica")[0].GetID());
        }

        [TestMethod]
        public void Stylist_GetClients_EmptyIfNoClients()
        {
            Stylist newStylist = new Stylist("Monica");
            newStylist.Save();

            Assert.AreEqual(0, newStylist.GetClients().Length);
        }

        [TestMethod]
        public void Stylist_GetClients_ReturnsCorrectClients()
        {
            Stylist newStylist = new Stylist("Susan");
            newStylist.Save();
            
            Client client1 = new Client("Sarah", newStylist);
            Client client2 = new Client("Irene", newStylist);
            client1.Save();
            client2.Save();

            Client[] clients = newStylist.GetClients();

            Assert.AreEqual(2, clients.Length);
        }
    }
}
