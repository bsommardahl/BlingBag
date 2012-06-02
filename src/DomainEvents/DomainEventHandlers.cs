using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DomainEvents
{
    public static class DomainEventHandlers
    {
        static readonly List<KeyValuePair<Type, Type>> Handlers = new List<KeyValuePair<Type, Type>>();

        public static Func<Type, dynamic> Resolve = x => Activator.CreateInstance(x);

        public static void Register<TEvent, THandler>() where THandler : IDomainEventHandler<TEvent>
        {
            Handlers.Add(new KeyValuePair<Type, Type>(typeof(TEvent), typeof(THandler)));
        }

        public static void Register(Type eventType, Type handlerType)
        {
            Handlers.Add(new KeyValuePair<Type, Type>(eventType, handlerType));
        }

        public static List<IDomainEventHandler<T>> GetFor<T>(T @event)
        {
            //get all handlers that match the actual type of @event
            
            var handlersTypes = Handlers
                .Where(x => x.Key.IsInstanceOfType(@event))
                .Select(x => (x.Value));

            var domainEventHandlers = handlersTypes.Select(x => Resolve(x)).Cast<IDomainEventHandler<T>>();

            return domainEventHandlers.ToList();
        }
    }
}