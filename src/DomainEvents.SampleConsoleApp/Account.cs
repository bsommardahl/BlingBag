namespace DomainEvents.SampleConsoleApp
{
    public class Account
    {
        public Account(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
        public event DomainEvent NotifyObservables;

        public void ChangeName(string newName)
        {
            var oldName = Name;
            Name = newName;

            NotifyObservables(new TheNameChanged {OldName = oldName, NewName = newName});
        }
    }
}