using System.Linq;
using DomainEvents.Testing;
using Machine.Specifications;

namespace DomainEvents.Specs
{
    public class when_initializing_domain_events_for_an_object_with_recursive_parent_and_children
    {
        static DomainEventInitializer _initializer;
        static TestDomainEventDispatcher _testDomainEventDispatcher;
        static OrgUnit _root;
        static OrgUnit _parent;
        static OrgUnit _firstChild;
        static OrgUnit _secondChild;

        Establish context = () =>
            {
                _testDomainEventDispatcher = new TestDomainEventDispatcher();
                _initializer = new DomainEventInitializer(_testDomainEventDispatcher);

                _parent = new OrgUnit {Id = 1, Parent = null};
                _root = new OrgUnit { Id = 2, Parent = _parent, };
                _firstChild = new OrgUnit {Id = 3, Parent = _root};
                _secondChild = new OrgUnit {Id = 4, Parent = _root};
                
                _root.Children.Add(_firstChild);
                _root.Children.Add(_secondChild);
            };

        Because of = () =>
            {
                _initializer.Initialize(_root);
                _root.Go();
                _root.Parent.Go();
                _root.Children.First().Go();
                _root.Children.Last().Go();
            };

        It should_have_dispatched_an_event_on_first_child =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<OrgUnit>()
                .ShouldContain(x => x == _firstChild);

        It should_have_dispatched_an_event_on_parent =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<OrgUnit>()
                .ShouldContain(x => x == _parent);

        It should_have_dispatched_an_event_on_second_child =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<OrgUnit>()
                .ShouldContain(x => x == _secondChild);

        It should_have_dispatched_an_event_on_the_root_object =
            () =>
            _testDomainEventDispatcher.WithEventsDispatched<OrgUnit>()
                .ShouldContain(x => x == _root);
    }
}