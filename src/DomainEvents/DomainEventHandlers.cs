using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainEvents
{
    public static class DomainEventHandlers
    {
        static readonly List<KeyValuePair<Type, Type>> Handlers = new List<KeyValuePair<Type, Type>>();

        public static Func<Type, dynamic> Resolve = x => Activator.CreateInstance(x);

        public static void Register<TEvent, THandler>() where THandler : IDomainEventHandler<TEvent>
        {
            Handlers.Add(new KeyValuePair<Type, Type>(typeof (TEvent), typeof (THandler)));
        }

        public static List<dynamic> GetFor<T>(T @event)
        {
            var handlersTypes = Handlers
                .Where(x => x.Key.IsAssignableFrom(@event.GetType()))
                .Select(x => (x.Value));

            var domainEventHandlers = handlersTypes.Select(x => Resolve(x));

            return domainEventHandlers.ToList();
        }
    }
}