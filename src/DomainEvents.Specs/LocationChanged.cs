namespace DomainEvents.Specs
{
    public class LocationChanged
    {
        public string NewLocation;

        public LocationChanged(string newLocation)
        {
            NewLocation = newLocation;
        }
    }
}