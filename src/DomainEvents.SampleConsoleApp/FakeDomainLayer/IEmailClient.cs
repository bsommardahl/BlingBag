namespace DomainEvents.SampleConsoleApp.FakeDomainLayer
{
    public interface IEmailClient
    {
        void Send(string emailAddress, string subject, string body);
    }
}