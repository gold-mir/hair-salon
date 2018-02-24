using Microsoft.AspNetCore.Mvc;
using System;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
        [HttpGet("/stylists")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/stylists/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet("/stylists/{id}/clients")]
        public ActionResult ClientList(int id)
        {
            return View();
        }

        [HttpGet("/stylists/{stylistID}/clients/{clientID}")]
        public ActionResult ClientInfo(int stylistID, int clientID)
        {
            return View();
        }
    }
}
