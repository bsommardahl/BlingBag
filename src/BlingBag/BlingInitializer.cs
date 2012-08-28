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
        readonly IEventSetter _eventSetter;

        public BlingInitializer(IBlingDispatcher dispatcher = null, IEventSetter eventSetter = null)
        {
            _dispatcher = dispatcher ?? (_dispatcher = new DefaultBlingDispatcher());
            _eventSetter = eventSetter ?? (_eventSetter = new DefaultEventSetter());
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
            _eventSetter.Set(obj, eventHandler, seen);
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

            foreach (object item in collection)
            {
                if (seen.Contains(item)) return;

                InitializeObject(item, seen, eventHandler);
            }
        }

        static bool IsCollectionType(Type type)
        {
            return typeof (IEnumerable).IsAssignableFrom(type) && type != typeof (string);
        }

        bool IsClassThatHasDomainEvents(PropertyInfo prop)
        {
            return prop.PropertyType.GetEvents().Any(x => x.EventHandlerType.Name.StartsWith("Blinger"));
        }
    }
}