using Microsoft.AspNetCore.Mvc;
using System;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
        [HttpGet("/clients")]
        public ActionResult Index()
        {
            return View(Client.GetAll());
        }

        [HttpGet("/clients/new")]
        public ActionResult New()
        {
            return View(Stylist.GetAll());
        }

        [HttpPost("/clients")]
        public ActionResult Create()
        {
            string name = Request.Form["client-name"];
            int id = int.Parse(Request.Form["stylist-select"]);

            Stylist stylist = Stylist.GetByID(id);

            if(stylist == null || name == "")
            {
                return View("Error");
            }

            Client client = new Client(name, stylist);
            client.Save();
            return View("Index", Client.GetAll());
        }

        [HttpGet("/clients/{id}")]
        public ActionResult Details(int id)
        {
            Client client = Client.GetByID(id);
            if(client != null)
            {
                return View(client);
            } else {
                Response.StatusCode = 404;
                return View("NotFound");
            }
        }

        [HttpPost("/clients/clear")]
        public ActionResult Clear()
        {
            Client.DeleteAll();
            return Redirect("/clients");
        }

        [HttpPost("/clients/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Client client = Client.GetByID(id);

            if(client != null)
            {
                client.Delete();
            }

            return Redirect("/clients");
        }

        [HttpGet("/clients/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Client client = Client.GetByID(id);
            if(client != null)
            {
                return View(client);
            } else {
                Response.StatusCode = 404;
                return View("NotFound");
            }
        }

        [HttpPost("/clients/{id}/update")]
        public ActionResult Update(int id)
        {
            Client client = Client.GetByID(id);
            if(client != null)
            {
                int newStylistID = int.Parse(Request.Form["stylist-select"]);
                Stylist newStylist = Stylist.GetByID(newStylistID);
                if(newStylist != null)
                {
                    client.SetStylist(newStylist);
                }

                string newName = Request.Form["name-input"];
                client.SetName(newName);

                return Redirect($"/clients/{id}");
            } else {
                Response.StatusCode = 500;
                return View("Error");
            }
        }
    }
}
