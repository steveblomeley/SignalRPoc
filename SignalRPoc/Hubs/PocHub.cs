using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using SignalRPoc.PerfCounters;

namespace SignalRPoc.Hubs
{
    public class PocHub : Hub
    {
        public PocHub()
        {
            StartCounterCollection();
        }

        private void StartCounterCollection()
        {
            var task = Task.Factory.StartNew(async () =>
            {
                var perfService = new PerfCounterService();
                while (true)
                {
                    var results = perfService.GetResults();
                    Clients.All.newCounters(results);
                    await Task.Delay(2000);
                }
            }, TaskCreationOptions.LongRunning);
        }

        public void Send(string message)
        {
            Clients.All.newMessage($"{Context.User.Identity.Name} says {message}");
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
    }
}