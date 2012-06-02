namespace DomainEvents.StructureMap.Specs
{
    public class Location
    {
        public string Address { get; private set; }

        public event DomainEvent RaiseEvent;

        public void ChangeLocation(string address)
        {
            Address = address;
            RaiseEvent(new LocationChanged());
        }
    }
}