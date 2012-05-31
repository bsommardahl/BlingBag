using System;
using System.ComponentModel;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace DomainEvents.Specs
{
    public class when_dispatching_an_event_with_a_registered_handler
    {
        static Mock<IDomainEventHandler> _handler;
        static IDomainEventDispatcher _domainEventDispatcher;
        static Mock<object> _event;

        Establish context = () =>
            {
                _event = new Mock<object>();
                _handler = new Mock<IDomainEventHandler>();

                DomainEventHandlers.Resolve = typeToResolve => _handler.Object;                
                DomainEventHandlers.Register(_event.Object.GetType(), _handler.Object.GetType());

                _handler.Setup(x => x.Handles).Returns(_event.Object.GetType());

                _domainEventDispatcher = new DefaultDomainEventDispatcher();
            };

        Because of = () => _domainEventDispatcher.Dispatch(_event.Object);

        It should_dispatch_the_event_using_the_expected_handler =
            () => _handler.Verify(x => x.Handle(Moq.It.Is<object>(y => y == _event.Object)));
    }
}