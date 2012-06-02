using System;
using System.Collections;
using System.Reflection;
using StructureMap;

namespace DomainEvents.StructureMap
{
    public class StructureMapDomainEventDispatcher : IDomainEventDispatcher
    {
        readonly IContainer _container;

        public StructureMapDomainEventDispatcher(IContainer container)
        {
            _container = container;
        }

        #region IDomainEventDispatcher Members

        public void Dispatch(object @event)
        {
            Type handlerType = typeof (IDomainEventHandler<>);
            Type genericHandlerType = handlerType.MakeGenericType(@event.GetType());
            IList domainEventHandlers = _container.GetAllInstances(genericHandlerType);

            foreach (object handler in domainEventHandlers)
            {
                MethodInfo handlerMethod = handler.GetType().GetMethod("Handle");
                handlerMethod.Invoke(handler, new[] {@event});
            }
        }

        #endregion
    }
}