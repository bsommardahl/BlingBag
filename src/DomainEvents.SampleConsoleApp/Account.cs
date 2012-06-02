namespace DomainEvents.SampleConsoleApp
{
    public class Account
    {
        public Account(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        
        public event DomainEvent NotifyObservers;

        public void ChangeName(string newName)
        {
            var oldName = Name;
            Name = newName;

            NotifyObservers(new TheNameChanged {OldName = oldName, NewName = newName});
        }
    }
}