using BlingBag.Testing;
using Machine.Specifications;

namespace BlingBag.Specs
{
    public class when_initializing_a_complex_object_with_nulls
    {
        static User _user;
        static TestDispatcher _testDispatcher;
        static BlingInitializer _initializer;
        static object _eventRaisedByUser;
        static object _eventRaisedByAccount;
        static Account _account;

        Establish context = () =>
            {
                _testDispatcher = new TestDispatcher();
                _initializer = new BlingInitializer(_testDispatcher);

                _account = new Account();
                _account.NotifyObservers += x => _eventRaisedByAccount = x;

                _user = new User {Account = _account};
                _user.Bling += x => _eventRaisedByUser = x;
            };

        Because of = () => _initializer.Initialize(_user);

        It should_initialize_the_account = () =>
            {
                _account.DoSomething();
                _testDispatcher.EventsDispatched.ShouldContain("account did something");
            };

        It should_initialize_the_user = () =>
            {
                _user.DoSomething();
                _testDispatcher.EventsDispatched.ShouldContain("user did something");
            };
    }
}