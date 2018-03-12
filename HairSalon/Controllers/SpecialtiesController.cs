using Microsoft.AspNetCore.Mvc;
using System;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialtiesController : Controller
    {
        [HttpGet("/specialties")]
        public ActionResult Index()
        {
            return View(Specialty.GetAll());
        }

        [HttpGet("/specialties/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/specialties")]
        public ActionResult Create()
        {
            string name = Request.Form["specialty-name"];
            if(name == "")
            {
                return View("New", "Name cannot be empty.");
            } else {
                Specialty specialty = new Specialty(name);
                specialty.Save();
                return Redirect("/specialties");
            }
        }

        [HttpGet("/specialties/{id}")]
        public ActionResult Details(int id)
        {
            Specialty spec = Specialty.GetByID(id);
            if(spec != null)
            {
                return View(spec);
            } else {
                Response.StatusCode = 404;
                return View("NotFound");
            }
        }

        [HttpPost("/specialties/{id}/addStylist")]
        public ActionResult AddStylist(int id)
        {
            Specialty spec = Specialty.GetByID(id);
            if(spec != null)
            {
                int stylistID = int.Parse(Request.Form["stylist-select"]);
                Stylist stylist = Stylist.GetByID(stylistID);
                if(stylist != null)
                {
                    stylist.AddSpecialty(spec);
                    return Redirect($"/specialties/{id}");
                } else {
                    Response.StatusCode = 500;
                    return View("Error");
                }
            } else {
                Response.StatusCode = 500;
                return View("Error");
            }
        }
    }
}
