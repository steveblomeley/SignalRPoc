using System.Collections.Generic;
using System.Web.Http;
using SignalRPoc.App_Data;
using SignalRPoc.Models;

namespace SignalRPoc.Controllers
{
    public class SessionsController : ApiController
    {
        private readonly ISessionStore _lockStore;

        public SessionsController(ISessionStore lockStore)
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