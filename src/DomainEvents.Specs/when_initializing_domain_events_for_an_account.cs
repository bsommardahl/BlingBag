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
                var locationInList1 = new Location();
                var locationInList2 = new Location();
                var location = new Location
                    {
                        Account = new Account
                            {
                                Location = new Location
                                    {
                                        Account = _account
                                    },
                                    OldLocations = new List<Location>
                                        {
                                            locationInList1,
                                            locationInList2,
                                        }
                            },
                        Account2 = null,
                    };
                _account = new Account
                    {
                        Location = location,                        
                    };                
            };

        Because of = () =>
            {
                _initializer.Initialize(_account);
                _account.ChangeName("something else");
                _account.Location.ChangeLocation("my house");
                _account.Location.Account.ChangeName("changing");
                _account.Location.Account.Location.ChangeLocation("something else again");                
            };

        It should_have_dispatched_an_event_on_a_parent_object =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<NameChanged>()
                .ShouldContain(x => x.NewName == "something else");

        It should_have_dispatched_an_event_on_a_child_object =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<LocationChanged>()
                .ShouldContain(x => x.NewLocation == "my house");

        It should_have_dispatched_an_event_on_a_child_of_a_child_object =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<NameChanged>()
                .ShouldContain(x => x.NewName == "changing");

        It should_have_dispatched_an_event_on_a_child_of_a_child_of_a_child_object =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<LocationChanged>()
                .ShouldContain(x => x.NewLocation == "something else again");
    }
}