using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainEvents.Testing
{
    public class TestDomainEventDispatcher : IDomainEventDispatcher
    {
        public List<object> EventsDispatched = new List<object>();

        public void Dispatch(object @event)
        {
            EventsDispatched.Add(@event);
        }
       
        public T ShouldHaveDispatchedAtLeastOnce<T>()
        {
            var @event = EventsDispatched.FirstOrDefault(x => x.GetType() == typeof (T));
            if (@event==null)
            {
                throw new EventNotDispatchedException<T>();
            }
            return (T) @event;
        }

        public List<T> WithEventsDispatched<T>()
        {
            return EventsDispatched.Where(x => x.GetType() == typeof (T)).Cast<T>().ToList();
        }
    }

    public static class WithExtensions
    {
        public static void With<T>(this T obj, Func<T, bool> isTrue)
        {
            if(!isTrue(obj))
            {
                throw new Exception("The target object didn't have the expected values.");
            }
        }
    }
}