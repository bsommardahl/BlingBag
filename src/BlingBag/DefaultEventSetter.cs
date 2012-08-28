using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlingBag
{
    public class DefaultEventSetter : IEventSetter
    {
        public void Set<TClass>(TClass obj, object @delegate, HashSet<object> seen) where TClass : class
        {
            if (obj == null) return;

            if (seen.Contains(obj)) return;

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            Func<EventInfo, FieldInfo> getField =
                ei => obj.GetType().GetField(ei.Name, bindingFlags);

            var domainEventInfos =
                obj.GetType().GetEvents(bindingFlags).Where(
                    x => x.EventHandlerType == typeof (Blinger));

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
    }
}