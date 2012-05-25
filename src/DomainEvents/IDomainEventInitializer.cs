using System;

namespace AcklenAvenue.DomainEvents
{
    public interface IDomainEventInitializer
    {
        void Initialize<T>(T obj, DomainEvent eventHandler);
    }
}