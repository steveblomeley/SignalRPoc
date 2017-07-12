using System;
using System.Collections.Generic;
using SignalRPoc.Models;

namespace SignalRPoc.App_Data
{
    public interface ILockStore
    {
        void Add(Session session);
        void Delete(Session session);
        void DeleteWhere(Func<Session, bool> predicate);
        IEnumerable<Session> GetAll();
    }
}