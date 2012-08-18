using System.Linq;
using BlingBag.Testing;
using Machine.Specifications;

namespace BlingBag.Specs
{
    public class when_initializing_blingers_for_an_object_with_recursive_parent_and_children
    {
        static BlingInitializer _initializer;
        static TestDispatcher _testDispatcher;
        static OrgUnit _root;
        static OrgUnit _parent;
        static OrgUnit _firstChild;
        static OrgUnit _secondChild;

        Establish context = () =>
            {
                _testDispatcher = new TestDispatcher();
                _initializer = new BlingInitializer(_testDispatcher);

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
            _testDispatcher.WithEventsDispatched<OrgUnit>()
                .ShouldContain(x => x == _firstChild);

        It should_have_dispatched_an_event_on_parent =
            () =>
            _testDispatcher.WithEventsDispatched<OrgUnit>()
                .ShouldContain(x => x == _parent);

        It should_have_dispatched_an_event_on_second_child =
            () =>
            _testDispatcher.WithEventsDispatched<OrgUnit>()
                .ShouldContain(x => x == _secondChild);

        It should_have_dispatched_an_event_on_the_root_object =
            () =>
            _testDispatcher.WithEventsDispatched<OrgUnit>()
                .ShouldContain(x => x == _root);
    }
}