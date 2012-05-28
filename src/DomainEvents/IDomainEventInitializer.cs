namespace DomainEvents
{
    public interface IDomainEventInitializer
    {
        void Initialize<T>(T obj) where T : class;
    }
}