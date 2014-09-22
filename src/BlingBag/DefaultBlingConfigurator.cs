using System;
using System.Reflection;

namespace BlingBag
{
    public class DefaultBlingConfigurator : IBlingConfigurator<Action<object>>
    {
        readonly IBlingDispatcher _blingDispatcher;

        public DefaultBlingConfigurator(IBlingDispatcher blingDispatcher)
        {
            _blingDispatcher = blingDispatcher;
        }

        #region IBlingConfigurator<Action<object>> Members

        public Func<EventInfo, bool> EventSelector
        {
            get { return x => x.EventHandlerType == typeof (Action<object>); }
        }

        public Action<object> HandleEvent
        {
            get { return @event => _blingDispatcher.Dispatch(@event); }
        }

        public object GetHandler(object obj)
        {
            return obj;
        }

        #endregion
    }
}