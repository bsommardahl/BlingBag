namespace DomainEvents
{
    public class DefaultDomainEventDispatcher :IDomainEventDispatcher
    {
        public void Dispatch(object @event)
        {
            var handlers = DomainEventHandlers.GetFor(@event);
            handlers.ForEach(x => x.Handle(@event));
        }
    }
}