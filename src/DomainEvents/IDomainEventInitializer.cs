namespace DomainEvents
{
    public interface IDomainEventInitializer
    {
        T Initialize<T>(T obj) where T : class;
    }
}