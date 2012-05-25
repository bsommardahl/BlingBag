namespace DomainEvents.Specs
{
    public class Account
    {
        public string Name { get; private set; }

        public Location Location { get; set; }

        public event DomainEvent NotifyObservers;

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
    }
}