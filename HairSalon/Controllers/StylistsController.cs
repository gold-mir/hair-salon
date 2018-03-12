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
            return View(Stylist.GetAll());
        }

        [HttpGet("/stylists/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/stylists")]
        public ActionResult Create()
        {
            string name = Request.Form["stylist-name"];
            if(name == "")
            {
                return View("New", "Name cannot be empty.");
            } else {
                Stylist stylist = new Stylist(name);
                stylist.Save();
                return View("Index", Stylist.GetAll());
            }
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult Details(int id)
        {
            Stylist stylist = Stylist.GetByID(id);
            if(stylist != null)
            {
                return View(stylist);
            } else {
                Response.StatusCode = 404;
                return View("NotFound");
            }
        }

        [HttpGet("/stylists/{stylistID}/clients/{clientID}")]
        public ActionResult ClientInfo(int stylistID, int clientID)
        {
            Stylist stylist = Stylist.GetByID(stylistID);
            Client client = Client.GetByID(clientID);

            if(stylist != null && client != null)
            {
                return View(client);
            } else {
                Response.StatusCode = 404;
                return View("NotFound");
            }
        }

        [HttpPost("/stylists/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Stylist stylist = Stylist.GetByID(id);
            if(stylist != null)
            {
                stylist.Delete();
            }
            return Redirect("/stylists");
        }

        [HttpPost("/stylists/clear")]
        public ActionResult Clear()
        {
            Stylist.DeleteAll();
            return Redirect("/stylists");
        }
    }
}
