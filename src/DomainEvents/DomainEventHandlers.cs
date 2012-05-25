using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainEvents
{
    public static class DomainEventHandlers
    {
        static readonly IDictionary<Type, object> Handlers = new Dictionary<Type, object>();

        public static void Register<T>(IDomainEventHandler<T> handler)
        {
            Handlers.Add(typeof(T), handler);
        }

        public static List<IDomainEventHandler<T>> GetFor<T>()
        {
            var handlers = Handlers
                .Where(x => x.Key == typeof(T))
                .Select(x => ((IDomainEventHandler<T>)x.Value));

            return handlers.ToList();
        }
    }
}