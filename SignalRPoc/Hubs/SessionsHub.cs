using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalRPoc.App_Data;

namespace SignalRPoc.Hubs
{
    public class SessionsHub : Hub
    {
        public override Task OnDisconnected(bool stopCalled)
        {
            var sessions = AllSessions.List.Where(s => s.SignalRClientId == Context.ConnectionId).ToList();

            if (sessions.Any())
            {
                foreach (var session in sessions)
                {
                    AllSessions.List.Remove(session);
                }

                Clients.All.sessionsChanged();
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}