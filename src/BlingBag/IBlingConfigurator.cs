using System;
using System.Reflection;

namespace BlingBag
{
    public interface IBlingConfigurator
    {
        Func<EventInfo, bool> EventSelector { get; }

        object HandleEvent { get; }
    }
}