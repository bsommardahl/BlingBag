using System;

namespace DomainEvents.Specs
{
    public class Location
    {
        public string Address { get; private set; }

        public Account Account { get; set; }

        public Account Account2 { get; set; }

        public event DomainEvent RaiseEvent;

        public void ChangeLocation(string address)
        {
            Address = address;
            RaiseEvent(new LocationChanged());
        }
    }
}