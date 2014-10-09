using System;
using System.Reflection;

namespace BlingBag
{
    public interface IBlingConfigurator<out TEventType>
    {
        Func<EventInfo, bool> EventSelector { get; }
        TEventType HandleEvent { get; }
        object GetHandler(object obj);
        
    }
}