using System.Collections;
using System.Reflection;

namespace DomainEvents
{
    public class DefaultDomainEventDispatcher : IDomainEventDispatcher
    {
        #region IDomainEventDispatcher Members

        public void Dispatch(object @event)
        {
            MethodInfo method = typeof (DomainEventHandlers).GetMethod("GetFor");
            MethodInfo generic = method.MakeGenericMethod(@event.GetType());
            var handlers = (IList) generic.Invoke(null, new[] {@event});

            foreach (object handler in handlers)
            {
                MethodInfo handlerMethod = handler.GetType().GetMethod("Handle");
                handlerMethod.Invoke(handler, new[] {@event});
            }
        }

        #endregion
    }
}