namespace BlingBag.StructureMap.Specs
{
    public class TestLocationChangedHandler : IBlingHandler<LocationChanged>
    {
        public void Handle(LocationChanged @event)
        {
            Handled = @event;
        }

        public LocationChanged Handled;
    }
}