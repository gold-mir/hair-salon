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
            return View();
        }

        [HttpGet("/clients/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/clients")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet("/clients/{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
