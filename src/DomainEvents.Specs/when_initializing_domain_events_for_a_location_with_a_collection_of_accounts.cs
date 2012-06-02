using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DomainEvents.Testing;
using Machine.Specifications;

namespace DomainEvents.Specs
{
    public class when_initializing_domain_events_for_a_location_with_a_collection_of_accounts
    {
        static DomainEventInitializer _initializer;
        static TestDomainEventDispatcher _testDomainEventDispatcher;
        static Location _location;

        Establish context = () =>
            {
                _testDomainEventDispatcher = new TestDomainEventDispatcher();
                _initializer = new DomainEventInitializer(_testDomainEventDispatcher);

                _location = new Location
                    {
                        CollectionOfAccounts = new Collection<Account>
                            {
                                new Account {},
                                new Account {},
                            }
                    };
            };

        Because of = () =>
            {
                _initializer.Initialize(_location);
                _location.CollectionOfAccounts.First().ChangeName("new name #1");
                _location.CollectionOfAccounts.Last().ChangeName("new name #2");
            };

        It should_have_dispatched_an_event_on_a_collection_member =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<NameChanged>()
                .ShouldContain(x => x.NewName == "new name #1");

        It should_have_dispatched_an_event_on_another_collection_member =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<NameChanged>()
                .ShouldContain(x => x.NewName == "new name #2");
    }
}