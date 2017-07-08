using System.Collections.Generic;
using System.Web.Http;
using SignalRPoc.App_Data;
using SignalRPoc.Models;

namespace SignalRPoc.Controllers
{
    public class SessionsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Session> Get()
        {
            return AllSessions.List;
        }
    }
}