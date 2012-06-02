using System;

namespace DomainEvents.Specs
{
    public class TestHandler : IDomainEventHandler<TestClass>
    {
        public object EventHandled;

        public void Handle(TestClass @event)
        {
            EventHandled = @event;
        }        
    }
}