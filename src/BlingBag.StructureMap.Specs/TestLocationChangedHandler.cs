namespace BlingBag.StructureMap.Specs
{
    public class TestLocationChangedHandler : IBlingHandler<LocationChanged>
    {
        public LocationChanged Handled;

        #region IBlingHandler<LocationChanged> Members

        public void Handle(LocationChanged @event)
        {
            Handled = @event;
        }

        #endregion
    }
}