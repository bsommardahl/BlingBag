namespace DomainEvents
{
    public class DefaultDomainEventDispatcher : IDomainEventDispatcher
    {
        public void Dispatch<T>(T @event)
        {            
            foreach (var handler in DomainEventHandlers.GetFor<T>())
            {
                handler.Handle(@event);
            }
        }        
    }
}