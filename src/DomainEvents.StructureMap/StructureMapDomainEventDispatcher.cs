using System.Collections.Generic;
using System.Linq;
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
            var handlerType = typeof (IDomainEventHandler<>);
            var genericHandlerType = handlerType.MakeGenericType(@event.GetType());
            var domainEventHandlers = _container.GetAllInstances(genericHandlerType);

            foreach (dynamic handler in domainEventHandlers)
            {
                handler.Handle(@event);
            }
        }

        #endregion
    }
}