using System.Collections.Generic;
using DomainEvents.Testing;
using Machine.Specifications;

namespace DomainEvents.Specs
{
    public class when_initializing_domain_events_for_an_account
    {
        static DomainEventInitializer _initializer;
        static Account _account;        
        static TestDomainEventDispatcher _testDomainEventDispatcher;

        Establish context = () =>
            {
                _testDomainEventDispatcher = new TestDomainEventDispatcher();
                _initializer = new DomainEventInitializer(_testDomainEventDispatcher);
                var location = new Location
                    {
                        Account = new Account
                            {
                                Location = new Location
                                    {
                                        Account = _account
                                    }
                            },
                    };
                _account = new Account
                    {
                        Location = location
                    };                
            };

        Because of = () =>
            {
                _initializer.Initialize(_account);
                _account.ChangeName("something else");
                _account.Location.ChangeLocation("my house");
                _account.Location.Account.ChangeName("changing");
                _account.Location.Account.Location.ChangeLocation("something else");
            };

        It should_have_initialized_the_notifier_in_the_child_object =
            () => _testDomainEventDispatcher.ShouldHaveDispatched<LocationChanged>();

        It should_have_initialized_the_notifier_in_the_parent_object =
            () => _testDomainEventDispatcher.ShouldHaveDispatched<NameChanged>();
    }
}