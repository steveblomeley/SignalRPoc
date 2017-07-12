using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRPoc.App_Data;
using SignalRPoc.Hubs;

namespace SignalRPoc.Filters
{
    public class ReleasesALockFilter : IActionFilter
    {
        private readonly ILockStore _lockStore;

        public ReleasesALockFilter(ILockStore lockStore)
        {
            _lockStore = lockStore;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var httpContext = actionExecutedContext.HttpContext;
            var user = httpContext.User.Identity.Name;
            var recordId = int.Parse(httpContext.Request.Form["Model.Id"]);
            var signalRClientId = httpContext.Request.Form["SignalRClientId"];

            _lockStore.DeleteWhere(
                x => x.User == user && x.RecordId == recordId && x.SignalRClientId == signalRClientId);

            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();
        }
    }
}