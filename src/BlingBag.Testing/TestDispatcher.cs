using System.Collections.Generic;
using System.Linq;

namespace BlingBag.Testing
{
    public class TestDispatcher : IBlingDispatcher
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
}