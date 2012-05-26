using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace DomainEvents.Specs
{
    public class when_dispatching_a_registered_action_event
    {
        static DefaultDomainEventDispatcher _dispatcher;
        static Mock<TestClass> _event;
        static TestHandler _testHandler;

        Establish context = () =>
            {
                _dispatcher = new DefaultDomainEventDispatcher();

                DomainEventHandlers.Register<TestClass, TestHandler>();
                
                _testHandler = new TestHandler();
                DomainEventHandlers.Resolve = x => _testHandler;

                _event = new Mock<TestClass>();
            };

        Because of = () => _dispatcher.Dispatch(_event.Object);

        It should_handle_the_event_using_the_provided_action =
            () => _testHandler.EventHandled.ShouldEqual(_event.Object);
    }
}