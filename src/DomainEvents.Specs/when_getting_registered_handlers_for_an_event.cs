using System.Collections.Generic;
using Machine.Specifications;

namespace DomainEvents.Specs
{
    public class when_getting_registered_handlers_for_an_event
    {
        static object _testClass;
        static List<object> _result;
        static List<IDomainEventHandler<TestClass>> _expectedListOfHandlers;

        Establish context = () =>
            {
                _testClass = new TestClass();

                DomainEventHandlers.Register<TestClass, TestHandler>();

                var instantiatedTestHandler = new TestHandler();
                DomainEventHandlers.Resolve = x => instantiatedTestHandler;

                _expectedListOfHandlers = new List<IDomainEventHandler<TestClass>>
                    {
                        instantiatedTestHandler,
                    };
            };

        Because of = () => _result = DomainEventHandlers.GetFor(_testClass);

        It should_return_the_expected_list_of_handlers = () => _result[0].ShouldEqual(_expectedListOfHandlers[0]);
    }
}