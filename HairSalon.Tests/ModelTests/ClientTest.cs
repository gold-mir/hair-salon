using System;
using MySql.Data.MySqlClient;
using HairSalon.Models;
using HairSalon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTest  : DBTest, IDisposable
    {
        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
        }

        public Stylist defaultStylist;

        public ClientTest()
        {
            defaultStylist = new Stylist("Brenda");
            defaultStylist.Save();
        }

        [TestMethod]
        public void Client_GetAllInitiallyEmpty()
        {
            Assert.AreEqual(0, Client.GetAll().Length);
        }

        [TestMethod]
        public void Client_SavesToDatabase()
        {
            Client newClient = new Client("Ramona", defaultStylist);

            newClient.Save();

            Assert.AreEqual(1, Client.GetAll().Length);
        }

        [TestMethod]
        public void Client_DoesNothingWhenSavingTwice()
        {
            Client doppelganger = new Client("The Doctor", defaultStylist);

            doppelganger.Save();
            doppelganger.Save();

            Assert.AreEqual(1, Client.GetAll().Length);
        }

        [TestMethod]
        public void Client_Save_ErrorsIfStylistIdWrong()
        {
            Stylist fakeGuy = new Stylist("Totally Real Person");
            Client sucker = new Client("Poor Sucker", fakeGuy);

            Exception ex = null;

            try
            {
                sucker.Save();
            } catch (Exception e)
            {
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void Client_GetStylist_ReturnsCorrectStylist()
        {
            Stylist newGuy = new Stylist("Paul");
            newGuy.Save();

            Client client1 = new Client("John", defaultStylist);
            Client client2 = new Client("Sasha", newGuy);

            Assert.AreEqual(defaultStylist.GetID(), client1.GetStylist().GetID());
            Assert.AreEqual(newGuy.GetID(), client2.GetStylist().GetID());
        }

        [TestMethod]
        public void Client_GetStylist_ErrorsOnBadID()
        {
            Stylist newGuy = new Stylist("Paul");
            Client client = new Client("Also Paul", newGuy);

            Exception ex = null;

            try
            {
                client.GetStylist();
            } catch (Exception e)
            {
                ex = e;
            }

            Assert.IsNotNull(ex);
            Assert.AreEqual("Stylist id is -1. Did you forget to save it first?", ex.Message);
        }

        [TestMethod]
        public void Client_GetByID_ReturnsCorrectResult()
        {
            Client newClient = new Client("Miranda", defaultStylist);
            newClient.Save();

            Client newClientFromDB = Client.GetByID(newClient.GetID());

            Assert.AreEqual(newClient.GetName(), newClientFromDB.GetName());
        }

        [TestMethod]
        public void Client_GetClientsOfStylist_ReturnsCorrectResults()
        {
            Client client1 = new Client("Miranda", defaultStylist);
            Client client2 = new Client("Sarah", defaultStylist);
            client1.Save();
            client2.Save();

            Client[] clients = Client.GetClientsOfStylist(defaultStylist);

            Assert.AreEqual(2, clients.Length);
        }
    }
}
