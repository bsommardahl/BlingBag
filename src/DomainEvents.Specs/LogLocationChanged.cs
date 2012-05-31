using System;

namespace DomainEvents.Specs
{
    public class LogLocationChanged : IDomainEventHandler
    {
        public Type Handles
        {
            get { return typeof (LocationChanged); }
        }

        public void Handle(object @event)
        {
            
        }
    }
}