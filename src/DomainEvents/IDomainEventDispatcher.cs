namespace DomainEvents
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<T>(T @event);        
    }
}