using System;

namespace DomainEvents.Specs
{
    public class NameChangedHandler : IDomainEventHandler
    {
        public Type Handles
        {
            get { return typeof (NameChanged); }
        }

        public void Handle(object @event)
        {
            
        }
    }
}