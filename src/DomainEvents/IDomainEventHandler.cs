using System;

namespace DomainEvents
{
    public interface IDomainEventHandler
    {
        Type Handles { get; }
        void Handle(object @event);
    }

    
}