using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalRPoc.App_Data;

namespace SignalRPoc.Hubs
{
    public class SessionsHub : Hub
    {
        private readonly ISessionStore _lockStore;

        public SessionsHub(ISessionStore lockStore)
        {
            _lockStore = lockStore;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _lockStore.DeleteWhere(s => s.SignalRClientId == Context.ConnectionId);

            Clients.All.sessionsChanged();
            
            return base.OnDisconnected(stopCalled);
        }
    }
}