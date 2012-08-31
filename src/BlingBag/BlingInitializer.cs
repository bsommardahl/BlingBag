using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlingBag
{
    public class BlingInitializer : IBlingInitializer
    {
        readonly IBlingConfigurator _blingConfigurator;

        public BlingInitializer(IBlingConfigurator blingConfigurator)
        {
            _blingConfigurator = blingConfigurator;
        }

        #region IBlingInitializer Members

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

        void InitializeObject<TClass, TEventType>(TClass obj, HashSet<object> seen, TEventType eventHandler) where TClass : class
        {
            Set(obj, eventHandler, seen);
            Dig(obj, eventHandler, seen);
        }

        void Set<TClass, TEventType>(TClass obj, TEventType @delegate, HashSet<object> seen) where TClass : class
        {
            if (obj == null) return;

            if (seen.Contains(obj)) return;

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            Func<EventInfo, FieldInfo> getField =
                ei => obj.GetType().GetField(ei.Name, bindingFlags);

            var domainEventInfos =
                obj.GetType().GetEvents(bindingFlags).Where(_blingConfigurator.EventSelector);

            var fields = domainEventInfos.Select(getField).ToList();

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

        void Dig<TClass, TEventType>(TClass obj, TEventType eventHandler, HashSet<object> seen) where TClass : class
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

        void InitializeItemsInCollection<TClass, TEventType>(TClass obj, TEventType eventHandler, HashSet<object> seen,
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
            return typeof (IEnumerable).IsAssignableFrom(type) && type != typeof (string);
        }

        bool IsClassThatHasDomainEvents(PropertyInfo prop)
        {
            return prop.PropertyType.GetEvents().Any(_blingConfigurator.EventSelector);
        }
    }
}