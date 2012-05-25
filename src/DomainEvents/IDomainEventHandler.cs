namespace AcklenAvenue.DomainEvents
{
    public interface IDomainEventHandler<in T> 
    {
        void Handle(T @event);
    }
}