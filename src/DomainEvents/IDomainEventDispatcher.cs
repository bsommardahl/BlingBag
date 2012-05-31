namespace DomainEvents
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(object @event);        
    }
}