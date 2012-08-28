using System.Collections.Generic;

namespace BlingBag.Specs
{
    public class Account
    {
        public string Name { get; private set; }

        public Location Location { get; set; }

        public IEnumerable<Location> OldLocations { get; set; }

        public event Blinger NotifyObservers;

        public void ChangeName(string newName)
        {
            Name = newName;
            var nameChanged = new NameChanged
                {
                    NewName = newName,
                    Account = this,
                };
            NotifyObservers(nameChanged);
        }

        public void DoSomething()
        {
            NotifyObservers("account did something");
        }
    }
}