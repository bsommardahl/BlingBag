using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainEvents
{
    public static class DomainEventHandlers
    {
        static readonly List<KeyValuePair<Type, Type>> Handlers = new List<KeyValuePair<Type, Type>>();

        public static Func<Type, object> Resolve = x => Activator.CreateInstance(x);

        public static void Register<TEvent, THandler>() where THandler : IDomainEventHandler<TEvent>
        {
            Handlers.Add(new KeyValuePair<Type, Type>(typeof (TEvent), typeof (THandler)));
        }

        public static List<IDomainEventHandler<T>> GetFor<T>(T @event)
        {
            var handlersTypes = Handlers
                .Where(x => x.Key == @event.GetType())
                .Select(x => (x.Value));

            return handlersTypes.Select(x => Resolve(x)).Cast<IDomainEventHandler<T>>().ToList();
        }
    }
}