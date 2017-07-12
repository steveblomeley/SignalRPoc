using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRPoc.App_Data;
using SignalRPoc.Hubs;

namespace SignalRPoc.Filters
{
    public class ClosesEditorForRecord : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var httpContext = actionExecutedContext.HttpContext;
            var user = httpContext.User.Identity.Name;
            var recordId = int.Parse(httpContext.Request.Form["Model.Id"]);
            var signalRClientId = httpContext.Request.Form["SignalRClientId"];

            var sessions = AllSessions.List
                .Where(x => x.User == user && x.RecordId == recordId && x.SignalRClientId == signalRClientId).ToList();
            foreach (var session in sessions)
            {
                AllSessions.List.Remove(session);
            }

            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}