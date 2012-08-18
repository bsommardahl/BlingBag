using System.Collections.ObjectModel;
using System.Linq;
using BlingBag.Testing;
using Machine.Specifications;

namespace BlingBag.Specs
{
    public class when_initializing_blingers_for_a_location_with_a_collection_of_accounts
    {
        static BlingInitializer _initializer;
        static TestDispatcher _testDispatcher;
        static Location _location;

        Establish context = () =>
            {
                _testDispatcher = new TestDispatcher();
                _initializer = new BlingInitializer(_testDispatcher);

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
            _testDispatcher.WithEventsDispatched<NameChanged>()
                .ShouldContain(x => x.NewName == "new name #1");

        It should_have_dispatched_an_event_on_another_collection_member =
            () =>
            _testDispatcher.WithEventsDispatched<NameChanged>()
                .ShouldContain(x => x.NewName == "new name #2");
    }
}