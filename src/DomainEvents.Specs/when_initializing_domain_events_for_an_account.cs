using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;

namespace DomainEvents.Specs
{
    public class when_initializing_domain_events_for_an_account
    {
        static DomainEventInitializer _initializer;
        static Account _account;
        static List<object> _eventsRaised;

        Establish context = () =>
            {
                _initializer = new DomainEventInitializer();
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

                _eventsRaised = new List<object>();
            };

        Because of = () =>
            {
                _initializer.Initialize(_account, x => _eventsRaised.Add(x));
                _account.ChangeName("something else");
                _account.Location.ChangeLocation("my house");
                _account.Location.Account.ChangeName("changing");
                _account.Location.Account.Location.ChangeLocation("something else");
            };

        It should_have_initialized_the_notifier_in_the_child_object =
            () =>
                {
                    _eventsRaised.Any(x => x.GetType().Equals(typeof(LocationChanged))).ShouldBeTrue();
                };

        It should_have_initialized_the_notifier_in_the_parent_object =
            () => _eventsRaised.Any(x => x.GetType().Equals(typeof (NameChanged))).ShouldBeTrue();
    }
}