using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRPoc.App_Data;
using SignalRPoc.Filters;
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
        [OpensEditorForRecord]
        public virtual PartialViewResult Edit(int id, string signalrClientId = "")
        {
            var data = _data.FirstOrDefault(x => x.Id == id);

            if (data == null) throw new HttpException(404, $"No data found with id={id}");

            var model = new ViewModel {Model = data, SignalRClientId = signalrClientId};

            return PartialView("_Edit", model);
        }

        [HttpPost]
        [ClosesEditorForRecord]
        public virtual JsonResult Edit(Model model, string signalrClientId)
        {
            if (model.Data.StartsWith("Fail"))
            {
                Response.StatusCode = 500;
                return Json(new {model.Id, Message = $"Could not save \"{model.Data}\""});
            }

            return Json(new {model.Id, Message = $"Saved \"{model.Data}\"", Cancelled=false});
        }

        [HttpPost]
        [ClosesEditorForRecord]
        public virtual JsonResult CancelEdit(Model model)
        {
            return Json(new { model.Id, Message = $"Cancelled edit of \"{model.Data}\"", Cancelled = true });
        }
    }
}