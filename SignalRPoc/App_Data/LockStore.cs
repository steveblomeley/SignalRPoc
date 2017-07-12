using System;
using System.Collections.Generic;
using System.Linq;
using SignalRPoc.Models;

namespace SignalRPoc.App_Data
{
    public class LockStore : ILockStore
    {
        private static readonly HashSet<Session> _sessions = new HashSet<Session>();

        public void Add(Session session)
        {
            _sessions.Add(session);
        }

        public void Delete(Session session)
        {
            _sessions.Remove(session);
        }

        public void DeleteWhere(Func<Session, bool> predicate)
        {
            foreach (var session in _sessions.Where(predicate))
            {
                _sessions.Remove(session);
            }
        }

        public IEnumerable<Session> GetAll()
        {
            return _sessions;
        }
    }
}