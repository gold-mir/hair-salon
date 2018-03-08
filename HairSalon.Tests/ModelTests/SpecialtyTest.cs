using System;
using MySql.Data.MySqlClient;
using HairSalon.Models;
using HairSalon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialtyTest : DBTest
    {
        [TestMethod]
        public void Specialty_GetAll_StartsAtZero()
        {
            int count = Specialty.GetAll().Length;

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void Specialty_Save_SavesToDB()
        {
            Specialty newspec = new Specialty("Specialty Name");
            newspec.Save();

            Assert.AreEqual(1, Specialty.GetAll().Length);
            Assert.AreNotEqual(0, newspec.GetID());
        }

        [TestMethod]
        public void Specialty_GetByID_GetsCorrectSpecialty()
        {
            Specialty newspec = new Specialty("Specialty Name");
            newspec.Save();
            int id = newspec.GetID();

            Specialty dbSpec = Specialty.GetByID(id);

            Assert.AreEqual(id, dbSpec.GetID());
        }

        [TestMethod]
        public void Specialty_Delete_DeletesFromDB()
        {
            Specialty newspec = new Specialty("Specialty Name");
            newspec.Save();
            int id = newspec.GetID();

            newspec.Delete();
            Specialty fromDB = Specialty.GetByID(id);

            Assert.IsNull(fromDB);
        }

        [TestMethod]
        public void Specialty_GetAllStylists_GetsAssociatedStylists()
        {
            Specialty newspec = new Specialty("Specialty Name");
            newspec.Save();
            Stylist newStylist = new Stylist("Miradna");
            newStylist.Save();
            newStylist.AddSpecialty(newspec);

            Stylist[] stylists = newspec.GetAllStylists();
            Assert.AreEqual(stylists[0].GetID(), newStylist.GetID());
            Assert.AreEqual(1, stylists.Length);
        }
    }
}
