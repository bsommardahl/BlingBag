namespace DomainEvents.StructureMap.Specs
{
    public class TestLocationChangedHandler : IDomainEventHandler<LocationChanged>
    {
        public void Handle(LocationChanged @event)
        {
            Handled = @event;
        }

        public LocationChanged Handled;
    }
}