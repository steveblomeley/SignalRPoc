using System;

namespace SignalRPoc.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TakesALockAttribute : Attribute { }
}