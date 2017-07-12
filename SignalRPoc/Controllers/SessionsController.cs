using System.Collections.Generic;
using System.Web.Http;
using SignalRPoc.App_Data;
using SignalRPoc.Models;

namespace SignalRPoc.Controllers
{
    public class SessionsController : ApiController
    {
        private readonly ILockStore _lockStore;

        public SessionsController(ILockStore lockStore)
        {
            _lockStore = lockStore;
        }

        // GET api/<controller>
        public IEnumerable<Session> Get()
        {
            return _lockStore.GetAll();
        }
    }
}