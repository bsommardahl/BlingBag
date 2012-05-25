namespace DomainEvents.Specs
{
    public class NameChanged
    {
        public string NewName { get; set; }

        public Account Account { get; set; }
    }
}