using BlingBag.SampleConsoleApp.FakeDomainLayer.Entities;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer.Events
{
    public class TheNameChanged
    {
        public Account Account;
        public string NewName;
        public string OldName;

        public TheNameChanged(Account account, string oldName, string newName)
        {
            Account = account;
            OldName = oldName;
            NewName = newName;
        }
    }
}