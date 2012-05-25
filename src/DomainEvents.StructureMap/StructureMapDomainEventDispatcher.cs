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

        #region IDispatcher Members

        public void Dispatch<T>(T @event)
        {
            var eventHandlers = _container.GetAllInstances<IDomainEventHandler<T>>().ToList();            
            eventHandlers.ForEach(handler => handler.Handle(@event));
        }

        #endregion
    }
}