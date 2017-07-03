using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRPoc.Hubs;

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

            _sessions.Add(new Session{User = HttpContext.User.Identity.Name, RecordId=model.Id});
            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Model model)
        {
            var user = HttpContext.User.Identity.Name;
            var session = _sessions.FirstOrDefault(x => x.User == user && x.RecordId == model.Id);
            if (session != null) _sessions.Remove(session);
            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Sessions()
        {
            return View(_sessions);
        }

        [HttpGet]
        public PartialViewResult SessionsPartial()
        {
            return PartialView("_Sessions", _sessions);
        }

        private static readonly IList<Session> _sessions = new List<Session>();
    }

    public class Model
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }

    public class Session
    {
        public string User { get; set; }
        public int RecordId { get; set; }
    }
}