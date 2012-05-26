using System.Collections.Generic;
using Machine.Specifications;

namespace DomainEvents.Specs
{
    public class when_changing_the_account_name
    {
        const string NewName = "Some new name";
        static Account _account;
        static object _eventRaised;

        Establish context = () => { _account = SimulateGettingAccountFromRepository(); };

        Because of = () => _account.ChangeName(NewName);

        It should_change_the_account_name = () => _account.Name.ShouldEqual(NewName);

        It should_include_the_account_in_the_name_changed_event =
            () => ((NameChanged) _eventRaised).Account.ShouldEqual(_account);

        It should_include_the_new_name_in_the_name_changed_event =
            () => ((NameChanged) _eventRaised).NewName.ShouldEqual(NewName);

        It should_notify_that_it_happened =
            () => _eventRaised.ShouldBeOfType<NameChanged>();

        static Account SimulateGettingAccountFromRepository()
        {
            var account = new Account();
            account.NotifyObservers += x => _eventRaised = x;
            return account;
        }
    }

    public class when_getting_registered_handlers_for_an_event
    {
        static TestClass _testClass;
        static List<IDomainEventHandler<TestClass>> _result;
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