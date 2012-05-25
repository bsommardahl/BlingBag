using Machine.Specifications;

namespace AcklenAvenue.DomainEvents.Specs
{
    public class when_changing_the_account_name
    {
        const string NewName = "Some new name";
        static Account _account;
        static object _eventRaised;

        Establish context = () =>
            {
                _account = SimulateGettingAccountFromRepository();
            };

        static Account SimulateGettingAccountFromRepository()
        {
            var account = new Account();
            account.NotifyObservers += x => _eventRaised = x;
            return account;
        }

        Because of = () => _account.ChangeName(NewName);

        It should_notify_that_it_happened = 
            () => _eventRaised.ShouldBeOfType<NameChanged>();

        It should_include_the_new_name_in_the_name_changed_event =
            () => ((NameChanged)_eventRaised).NewName.ShouldEqual(NewName);

        It should_include_the_account_in_the_name_changed_event =
            () => ((NameChanged)_eventRaised).Account.ShouldEqual(_account);

        It should_change_the_account_name = () => _account.Name.ShouldEqual(NewName);
    }
}