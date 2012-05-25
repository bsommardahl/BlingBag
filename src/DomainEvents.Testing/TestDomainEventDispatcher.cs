using System.Collections.Generic;
using System.Linq;

namespace AcklenAvenue.DomainEvents.Testing
{
    public class TestDomainEventDispatcher : IDomainEventDispatcher
    {
        readonly List<object> _list = new List<object>();

        public void Dispatch<T>(T @event)
        {
            _list.Add(@event);
        }
       
        public object ShouldHaveDispatched<T>()
        {
            var @event = _list.FirstOrDefault(x => x.GetType() == typeof (T));
            if (@event==null)
            {
                throw new EventNotDispatchedException<T>();
            }
            return @event;
        }
    }
}