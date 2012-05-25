using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace DomainEvents.Specs
{
    public class when_dispatching_a_registered_action_event
    {
        static ActionDomainEventDispatcher _dispatcher;
        static Mock<object> _event;
        static object _eventDispatched;

        Establish context = () =>
            {
                _dispatcher = new ActionDomainEventDispatcher();

                _dispatcher.Register<object>(x => { _eventDispatched = x; });

                _event = new Mock<object>();
            };

        Because of = () => _dispatcher.Dispatch(_event.Object);

        It should_handle_the_event_using_the_provided_action = () => _eventDispatched.ShouldEqual(_event.Object);
    }
}