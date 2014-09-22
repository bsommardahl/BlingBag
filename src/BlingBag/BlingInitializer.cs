using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlingBag
{
    public class BlingInitializer<TEventType> : IBlingInitializer<TEventType>
    {
        readonly IBlingConfigurator<TEventType> _blingConfigurator;

        public BlingInitializer(IBlingConfigurator<TEventType> blingConfigurator)
        {
            _blingConfigurator = blingConfigurator;
        }

        #region IBlingInitializer<TEventType> Members

        public TClass Initialize<TClass>(TClass obj) where TClass : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            var seen = new HashSet<object>();

            InitializeObject(obj, seen, _blingConfigurator.HandleEvent);
            return obj;
        }

        #endregion

        void InitializeObject<TClass>(TClass obj, HashSet<object> seen, TEventType eventHandler)
            where TClass : class
        {
            Set(obj, eventHandler, seen);
            Dig(obj, eventHandler, seen);
        }

        void Set<TClass>(TClass obj, TEventType @delegate, HashSet<object> seen) where TClass : class
        {
            if (obj == null) return;

            if (seen.Contains(obj)) return;

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            Func<EventInfo, FieldInfo> getField =
                ei => obj.GetType().GetField(ei.Name, bindingFlags);

            EventInfo[] events = _blingConfigurator.GetHandler(obj).GetType().GetEvents(bindingFlags);

            IEnumerable<EventInfo> selectedEvents = events.Where(_blingConfigurator.EventSelector);

            List<FieldInfo> fields = selectedEvents.Select(getField).ToList();

            fields.ForEach(x =>
                {
                    if (x == null)
                    {
                        //what does this mean when this happens? Might mean that there was a problem.
                        //... this is null on proxy objects returned by NHibernate.
                        return;
                    }

                    x.SetValue(obj, @delegate);
                });

            seen.Add(obj);
        }

        void Dig<TClass>(TClass obj, TEventType eventHandler, HashSet<object> seen) where TClass : class
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

        void InitializeItemsInCollection<TClass>(TClass obj, TEventType eventHandler, HashSet<object> seen,
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
            return prop.PropertyType.GetEvents().Any(_blingConfigurator.EventSelector);
        }
    }
}