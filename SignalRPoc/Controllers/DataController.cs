using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SignalRPoc.Models;

namespace SignalRPoc.Controllers
{
    public partial class DataController : Controller
    {
        private IEnumerable<Model> _data = new[]
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

        public virtual PartialViewResult Edit(int Id)
        {
            var data = _data.FirstOrDefault(x => x.Id == Id);

            if (data == null) throw new HttpException(404, $"No data found with id={Id}");

            return PartialView("_Edit", data);
        }

        //TODO: create a post method for edit that arbitrarily succeeds or fails 
        //(result could depend on whether the record ID is odd or even?)
    }
}