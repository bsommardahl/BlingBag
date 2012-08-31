using System;
using System.Reflection;

namespace BlingBag
{
    public class DefaultBlingConfigurator : IBlingConfigurator
    {
        readonly IBlingDispatcher _blingDispatcher;

        public DefaultBlingConfigurator(IBlingDispatcher blingDispatcher)
        {
            _blingDispatcher = blingDispatcher;
        }

        #region IBlingConfigurator Members

        public Func<EventInfo, bool> EventSelector
        {
            get { return x => x.EventHandlerType == typeof (Blinger); }
        }

        public object HandleEvent
        {
            get
            {
                Blinger eventHandler = @event => _blingDispatcher.Dispatch(@event);
                return eventHandler;
            }
        }

        #endregion
    }
}