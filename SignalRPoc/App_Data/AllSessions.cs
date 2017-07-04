using System.Collections.Generic;
using SignalRPoc.Models;

namespace SignalRPoc.App_Data
{
    public static class AllSessions
    {
        public static IList<Session> List { get; } = new List<Session>();
    }
}