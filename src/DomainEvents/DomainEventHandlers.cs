using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainEvents
{
    public static class DomainEventHandlers
    {
        static readonly IDictionary<Type, Type> Handlers = new Dictionary<Type, Type>();

        public static Func<Type, object> Resolve = x => Activator.CreateInstance(x);

        public static void Register<TEvent, THandler>() where THandler : IDomainEventHandler<TEvent>
        {
            Handlers.Add(typeof (TEvent), typeof (THandler));
        }

        public static List<IDomainEventHandler<T>> GetFor<T>()
        {
            var handlersTypes = Handlers
                .Where(x => x.Key == typeof (T))
                .Select(x => (x.Value));

            return handlersTypes.Select(x => Resolve(x)).Cast<IDomainEventHandler<T>>().ToList();
        }
    }
}