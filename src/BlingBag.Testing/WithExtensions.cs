using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlingBag.Testing
{
    public static class WithExtensions
    {
        public static Func<EventInfo, bool> EventSelector = x => true;

        public static void With<T>(this T obj, Func<T, bool> isTrue)
        {
            if (!isTrue(obj))
            {
                throw new Exception("The target object didn't have the expected values.");
            }
        }

        public static T WithObserver<T>(this T obj, object @event)
        {
            Func<EventInfo, FieldInfo> getField =
                ei => obj.GetType().GetField(ei.Name,
                                             BindingFlags.NonPublic |
                                             BindingFlags.Instance |
                                             BindingFlags.GetField);

            IEnumerable<EventInfo> domainEventInfos =
                obj.GetType().GetEvents().Where(EventSelector);
            List<FieldInfo> fields = domainEventInfos.Select(getField).ToList();
            fields.ForEach(x => x.SetValue(obj, @event));

            return obj;
        }
    }
}