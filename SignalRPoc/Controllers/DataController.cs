using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRPoc.App_Data;
using SignalRPoc.Hubs;
using SignalRPoc.Models;

namespace SignalRPoc.Controllers
{
    public partial class DataController : Controller
    {
        private readonly IEnumerable<Model> _data = new[]
        {
            new Model {Id = 0, Data = "some data"},
            new Model {Id = 1, Data = "more data"},
            new Model {Id = 2, Data = "another bit of data"},
            new Model {Id = 3, Data = "penultimate data"},
            new Model {Id = 4, Data = "final data"}
        };

        // GET: Data
        public virtual ActionResult Index()
        {
            return View(_data);
        }

        [HttpGet]
        public virtual PartialViewResult Edit(int id)
        {
            var data = _data.FirstOrDefault(x => x.Id == id);

            if (data == null) throw new HttpException(404, $"No data found with id={id}");

            AllSessions.List.Add(new Session { User = HttpContext.User.Identity.Name, RecordId = data.Id });
            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();

            return PartialView("_Edit", data);
        }

        [HttpPost]
        public virtual JsonResult Edit(Model model)
        {
            var user = HttpContext.User.Identity.Name;
            var session = AllSessions.List.FirstOrDefault(x => x.User == user && x.RecordId == model.Id);
            if (session != null) AllSessions.List.Remove(session);
            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();

            if (model.Data.StartsWith("Fail"))
            {
                Response.StatusCode = 500;
                return Json(new {model.Id, Message = $"Could not save \"{model.Data}\""});
            }

            return Json(new {model.Id, Message = $"Saved \"{model.Data}\"", Cancelled=false});
        }

        [HttpPost]
        public virtual JsonResult CancelEdit(Model model)
        {
            var user = HttpContext.User.Identity.Name;
            var session = AllSessions.List.FirstOrDefault(x => x.User == user && x.RecordId == model.Id);
            if (session != null) AllSessions.List.Remove(session);
            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();

            return Json(new { model.Id, Message = $"Cancelled edit of \"{model.Data}\"", Cancelled = true });
        }
    }
}