using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DomainEvents
{
    public class DomainEventInitializer : IDomainEventInitializer
    {
        readonly IDomainEventDispatcher _dispatcher;

        public DomainEventInitializer(IDomainEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        #region IDomainEventInitializer Members

        public void Initialize<TClass>(TClass obj) where TClass : class
        {
            if(obj==null)
            {
                throw new ArgumentNullException("obj");
            }
            var seen = new HashSet<object>();
            var eventHandler = new DomainEvent(@event => _dispatcher.Dispatch(@event));
            Set(obj, eventHandler, seen);
            Dig(obj, eventHandler, seen);
        }

        #endregion

        void Dig<TClass>(TClass obj, object eventHandler, HashSet<object> seen)  where TClass : class
        {
            if (obj == null) return;

            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (!HasDomainEvents(prop)) continue;

                var objectWithDomainEvents = prop.GetValue(obj, null);

                if (objectWithDomainEvents == null) continue;
                
                Set(objectWithDomainEvents, eventHandler, seen);
                Dig(objectWithDomainEvents, eventHandler, seen);
            }
        }

        bool HasDomainEvents(PropertyInfo prop)
        {
            return prop.PropertyType.GetEvents().Any(x => x.EventHandlerType.Name.StartsWith("DomainEvent"));
        }

        void Set<TClass>(TClass obj, object @delegate, HashSet<object> seen) where TClass : class
        {
            if (obj == null) return;

            if (seen.Contains(obj)) return;

            Func<EventInfo, FieldInfo> getField =
                ei => obj.GetType().GetField(ei.Name,
                                             BindingFlags.NonPublic |
                                             BindingFlags.Instance |
                                             BindingFlags.GetField);

            IEnumerable<EventInfo> domainEventInfos =
                obj.GetType().GetEvents().Where(x => x.EventHandlerType.Name.StartsWith("DomainEvent"));
            List<FieldInfo> fields = domainEventInfos.Select(getField).ToList();
            fields.ForEach(x => x.SetValue(obj, @delegate));

            seen.Add(obj);
        }
    }
}