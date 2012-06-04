using DomainEvents.SampleConsoleApp.FakeDomainLayer.Entities;

namespace DomainEvents.SampleConsoleApp.FakeDomainLayer.Events
{
    public class TheNameChanged
    {
        public TheNameChanged(Account account, string oldName, string newName)
        {
            Account = account;
            OldName = oldName;
            NewName = newName;
        }

        public Account Account;
        public string NewName;
        public string OldName;
    }
}