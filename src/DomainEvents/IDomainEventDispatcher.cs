using System;

namespace AcklenAvenue.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<T>(T @event);        
    }
}