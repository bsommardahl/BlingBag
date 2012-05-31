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
            var domainEventHandlers = _container.GetAllInstances<IDomainEventHandler>();
            var matchingDomainEventHandlers = domainEventHandlers.Where(x => x.Handles == @event.GetType());
            foreach (var handler in matchingDomainEventHandlers)
            {
                handler.Handle(@event);
            }
        }

        #endregion
    }
}