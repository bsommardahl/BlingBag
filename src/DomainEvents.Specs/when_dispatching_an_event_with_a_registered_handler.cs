using System;
using System.ComponentModel;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace DomainEvents.Specs
{
    public class when_dispatching_an_event_with_a_registered_handler
    {
        static Mock<IDomainEventHandler<LocationChanged>> _handler;
        static IDomainEventDispatcher _domainEventDispatcher;
        static LocationChanged _event;

        Establish context = () =>
            {
                _event = new LocationChanged();
                _handler = new Mock<IDomainEventHandler<LocationChanged>>();

                DomainEventHandlers.Resolve = typeToResolve => _handler.Object;                
                DomainEventHandlers.Register(_event.GetType(), _handler.Object.GetType());

                _domainEventDispatcher = new DefaultDomainEventDispatcher();
            };

        Because of = () => _domainEventDispatcher.Dispatch(_event);

        It should_dispatch_the_event_using_the_expected_handler =
            () => _handler.Verify(x => x.Handle(Moq.It.Is<LocationChanged>(y => y == _event)));
    }
}