using Machine.Specifications;
using StructureMap;

namespace BlingBag.StructureMap.Specs
{
    public class when_dispatching_an_event_with_handler_registered_in_the_container
    {
        static Container _container;
        static TestLocationChangedHandler _handler;
        static IBlingDispatcher _blingDispatcher;
        static LocationChanged _event;

        Establish context = () =>
            {
                _event = new LocationChanged();

                _handler = new TestLocationChangedHandler();

                _container = new Container();
                _container.Configure(x => x.For<IBlingHandler<LocationChanged>>().Use(_handler));

                _blingDispatcher = new StructureMapBlingDispatcher(_container);
            };

        Because of = () => _blingDispatcher.Dispatch(_event);

        It should_dispatch_the_event_using_the_expected_handler =
            () => _handler.Handled.ShouldEqual(_event);
    }
}