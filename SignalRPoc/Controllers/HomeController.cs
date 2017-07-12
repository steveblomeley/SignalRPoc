using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRPoc.App_Data;
using SignalRPoc.Hubs;
using SignalRPoc.Models;

namespace SignalRPoc.Controllers
{
    public partial class HomeController : Controller
    {
        private static int _recordId = 0;
        private readonly ISessionStore _lockStore;

        public HomeController(ISessionStore lockStore)
        {
            _lockStore = lockStore;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpGet]
        //public virtual ActionResult Edit()
        //{
        //    var model = new Model
        //    {
        //        Id = _recordId++,
        //        Data = "some data"
        //    };

        //    AllSessions.List.Add(new Session { User = HttpContext.User.Identity.Name, RecordId = model.Id });
        //    var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
        //    context.Clients.All.sessionsChanged();

        //    return View(model);
        //}

        //[HttpPost]
        //public virtual ActionResult Edit(Model model)
        //{
        //    var user = HttpContext.User.Identity.Name;
        //    var session = AllSessions.List.FirstOrDefault(x => x.User == user && x.RecordId == model.Id);
        //    if (session != null) AllSessions.List.Remove(session);
        //    var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
        //    context.Clients.All.sessionsChanged();

        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public virtual ActionResult Sessions()
        {
            return View(_lockStore.GetAll());
        }

        [HttpGet]
        public virtual PartialViewResult SessionsPartial()
        {
            return PartialView("_Sessions", _lockStore.GetAll());
        }
    }
}