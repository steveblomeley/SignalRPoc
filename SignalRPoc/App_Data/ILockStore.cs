using System;
using System.Collections.Generic;
using SignalRPoc.Models;

namespace SignalRPoc.App_Data
{
    public interface ISessionStore
    {
        void Add(Session session);
        void Delete(Session session);
        bool DeleteWhere(Func<Session, bool> predicate);
        IEnumerable<Session> GetAll();
    }
}