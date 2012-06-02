using System.Collections;
using System.Reflection;

namespace DomainEvents
{
    public class DefaultDomainEventDispatcher :IDomainEventDispatcher
    {
        public void Dispatch(object @event)
        {
            var method = typeof(DomainEventHandlers).GetMethod("GetFor");
            var generic = method.MakeGenericMethod(@event.GetType());
            var handlers = (IList) generic.Invoke(null, new[] {@event});

            foreach(dynamic handler in handlers)
            {
                handler.Handle(@event);
            }            
        }
    }
}