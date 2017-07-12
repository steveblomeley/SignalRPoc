using System;
using System.Collections.Generic;
using System.Linq;
using SignalRPoc.Models;

namespace SignalRPoc.App_Data
{
    public class LockStore : ILockStore
    {
        private static readonly HashSet<Session> Sessions = new HashSet<Session>();

        public void Add(Session session)
        {
            Sessions.Add(session);
        }

        public void Delete(Session session)
        {
            Sessions.Remove(session);
        }

        public bool DeleteWhere(Func<Session, bool> predicate)
        {
            var result = false;
            var sessionsToRemove = Sessions.Where(predicate).ToList();

            foreach (var session in sessionsToRemove)
            {
                Sessions.Remove(session);
                result = true;
            }

            return result;
        }

        public IEnumerable<Session> GetAll()
        {
            return Sessions;
        }
    }
}