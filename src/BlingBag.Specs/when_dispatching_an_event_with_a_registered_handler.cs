using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace BlingBag.Specs
{
    public class when_dispatching_an_event_with_a_registered_handler
    {
        static Mock<IBlingHandler<LocationChanged>> _handler;
        static IBlingDispatcher _blingDispatcher;
        static LocationChanged _event;

        Establish context = () =>
            {
                _event = new LocationChanged("some location");
                _handler = new Mock<IBlingHandler<LocationChanged>>();

                BlingHandlers.Resolve = typeToResolve => _handler.Object;
                BlingHandlers.Register(_event.GetType(), _handler.Object.GetType());

                _blingDispatcher = new DefaultBlingDispatcher();
            };

        Because of = () => _blingDispatcher.Dispatch(_event);

        It should_dispatch_the_event_using_the_expected_handler =
            () => _handler.Verify(x => x.Handle(Moq.It.Is<LocationChanged>(y => y == _event)));
    }
}