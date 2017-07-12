using System;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using SignalRPoc.App_Data;
using SignalRPoc.Hubs;
using SignalRPoc.Models;

namespace SignalRPoc.Filters
{
    public class TakesALockAttribute : Attribute { }

    public class TakesALockFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var httpContext = actionExecutedContext.HttpContext;
            var user = httpContext.User.Identity.Name;
            var recordId = int.Parse(httpContext.Request.RequestContext.RouteData.GetRequiredString("id"));
            var signalRClientId = httpContext.Request.QueryString["signalRClientId"];

            AllSessions.List.Add(new Session
            {
                User = user,
                RecordId = recordId,
                SignalRClientId = signalRClientId
            });

            var context = GlobalHost.ConnectionManager.GetHubContext<SessionsHub>();
            context.Clients.All.sessionsChanged();
        }
    }
}