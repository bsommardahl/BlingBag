using Machine.Specifications;
using Moq;
using StructureMap;
using It = Machine.Specifications.It;

namespace DomainEvents.StructureMap.Specs
{
    public class when_dispatching_an_event_with_handler_registered_in_the_container
    {
        static Container _container;
        static TestLocationChangedHandler _handler;
        static IDomainEventDispatcher _domainEventDispatcher;
        static LocationChanged _event;

        Establish context = () =>
            {
                _event = new LocationChanged();

                _handler = new TestLocationChangedHandler();

                _container = new Container();
                _container.Configure(x => x.For<IDomainEventHandler<LocationChanged>>().Use(_handler));

                _domainEventDispatcher = new StructureMapDomainEventDispatcher(_container);
            };

        Because of = () => _domainEventDispatcher.Dispatch(_event);

        It should_dispatch_the_event_using_the_expected_handler =
            () => _handler.Handled.ShouldEqual(_event);
    }
}