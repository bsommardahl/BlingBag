using System;
using System.Collections.Generic;

namespace DomainEvents
{
    public class ActionDomainEventDispatcher : IDomainEventDispatcher
    {
        readonly IDictionary<Type, Delegate> _handlers = new Dictionary<Type, Delegate>();

        #region IDispatcher Members

        public void Dispatch<T>(T @event)
        {
            if (!_handlers.ContainsKey(typeof(T))) return;
            var handler = (Action<T>)_handlers[typeof(T)];
            handler.Invoke(@event);
        }

        #endregion

        public void Register<T>(Action<T> action)
        {
            _handlers.Add(typeof (T), action);
        }
    }
}