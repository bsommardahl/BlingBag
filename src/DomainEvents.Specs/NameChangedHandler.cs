using System;

namespace DomainEvents.Specs
{
    public class NameChangedHandler : IDomainEventHandler<NameChanged>
    {
        public void Handle(NameChanged @event)
        {
            
        }
    }
}