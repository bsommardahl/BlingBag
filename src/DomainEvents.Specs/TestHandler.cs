using System;

namespace DomainEvents.Specs
{
    public class TestHandler : IDomainEventHandler
    {
        public object EventHandled;

        public void Handle(object @event)
        {
            EventHandled = @event;
        }

        public Type Handles
        {
            get { return typeof (TestClass); }
        }        
    }
}