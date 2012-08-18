using System.Collections.Generic;
using Machine.Specifications;

namespace BlingBag.Specs
{
    public class when_getting_registered_handlers_for_an_event
    {
        static TestClass _testClass;
        static List<IBlingHandler<TestClass>> _result;
        static List<IBlingHandler<TestClass>> _expectedListOfHandlers;

        Establish context = () =>
            {
                _testClass = new TestClass();

                BlingHandlers.Register<TestClass, TestHandler>();

                var instantiatedTestHandler = new TestHandler();
                BlingHandlers.Resolve = x => instantiatedTestHandler;

                _expectedListOfHandlers = new List<IBlingHandler<TestClass>>
                    {
                        instantiatedTestHandler,
                    };
            };

        Because of = () => _result = BlingHandlers.GetFor(_testClass);

        It should_return_the_expected_list_of_handlers = () => _result[0].ShouldEqual(_expectedListOfHandlers[0]);
    }
}