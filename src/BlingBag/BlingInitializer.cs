using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlingBag
{
    public class BlingInitializer : IBlingInitializer
    {
        readonly IBlingDispatcher _dispatcher;

        public BlingInitializer(IBlingDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        #region IBlingInitializer Members

        public TClass Initialize<TClass>(TClass obj) where TClass : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var seen = new HashSet<object>();
            var eventHandler = new Blinger(@event => _dispatcher.Dispatch(@event));
            InitializeObject(obj, seen, eventHandler);
            return obj;
        }

        #endregion

        void InitializeObject<TClass>(TClass obj, HashSet<object> seen, Blinger eventHandler) where TClass : class
        {
            Set(obj, eventHandler, seen);
            Dig(obj, eventHandler, seen);
        }

        void Dig<TClass>(TClass obj, Blinger eventHandler, HashSet<object> seen) where TClass : class
        {
            if (obj == null) return;

            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (IsCollectionType(prop.PropertyType))
                {
                    InitializeItemsInCollection(obj, eventHandler, seen, prop);
                }

                if (!IsClassThatHasDomainEvents(prop)) continue;

                object objectWithDomainEvents = prop.GetValue(obj, null);

                if (objectWithDomainEvents == null) continue;

                InitializeObject(objectWithDomainEvents, seen, eventHandler);
            }
        }

        void InitializeItemsInCollection<TClass>(TClass obj, Blinger eventHandler, HashSet<object> seen,
                                                 PropertyInfo prop) where TClass : class
        {
            var collection = (IEnumerable) prop.GetValue(obj, null);
            if (collection == null) return;

            foreach (var item in collection)
            {
                if (seen.Contains(item)) return;

                InitializeObject(item, seen, eventHandler);
            }
        }

        static bool IsCollectionType(Type type)
        {
            return typeof (IEnumerable).IsAssignableFrom(type);
        }

        bool IsClassThatHasDomainEvents(PropertyInfo prop)
        {
            return prop.PropertyType.GetEvents().Any(x => x.EventHandlerType.Name.StartsWith("Blinger"));
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
                obj.GetType().GetEvents().Where(x => x.EventHandlerType.Name.StartsWith("Blinger"));
            List<FieldInfo> fields = domainEventInfos.Select(getField).ToList();
            fields.ForEach(x =>
                {
                    if (x == null) return;
                    x.SetValue(obj, @delegate);
                });

            seen.Add(obj);
        }
    }
}