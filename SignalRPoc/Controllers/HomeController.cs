﻿using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRPoc.App_Data;
using SignalRPoc.Hubs;
using SignalRPoc.Models;

namespace SignalRPoc.Controllers
{
    public class HomeController : Controller
    {
        private static int _recordId = 0;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var model = new Model
            {
                Id = _recordId++,
                Data = "some data"
            };

            AllSessions.List.Add(new Session{User = HttpContext.User.Identity.Name, RecordId=model.Id});
            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Model model)
        {
            var user = HttpContext.User.Identity.Name;
            var session = AllSessions.List.FirstOrDefault(x => x.User == user && x.RecordId == model.Id);
            if (session != null) AllSessions.List.Remove(session);
            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Sessions()
        {
            return View(AllSessions.List);
        }

        [HttpGet]
        public PartialViewResult SessionsPartial()
        {
            return PartialView("_Sessions", AllSessions.List);
        }
    }
}