using System.Collections.Generic;
using Machine.Specifications;

namespace BlingBag.Specs
{
    public class when_initializing_blingers_for_an_account : given_a_blinger_initializer_context
    {
        static Account _account;

        Establish context = () =>
            {
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
                Initializer.Initialize(_account);
                _account.ChangeName("something else");
                _account.Location.ChangeLocation("my house");
                _account.Location.Account.ChangeName("changing");
                _account.Location.Account.Location.ChangeLocation("something else again");
            };

        It should_have_dispatched_an_event_on_a_child_object =
            () =>
            ShouldHaveHandled<LocationChanged>()
                .ShouldContain(x => x.NewLocation == "my house");

        It should_have_dispatched_an_event_on_a_child_of_a_child_object =
            () =>
            ShouldHaveHandled<NameChanged>()
                .ShouldContain(x => x.NewName == "changing");

        It should_have_dispatched_an_event_on_a_child_of_a_child_of_a_child_object =
            () =>
            ShouldHaveHandled<LocationChanged>()
                .ShouldContain(x => x.NewLocation == "something else again");

        It should_have_dispatched_an_event_on_a_parent_object =
            () =>
            ShouldHaveHandled<NameChanged>()
                .ShouldContain(x => x.NewName == "something else");
    }
}