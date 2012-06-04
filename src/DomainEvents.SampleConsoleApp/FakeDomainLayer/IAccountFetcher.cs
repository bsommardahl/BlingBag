using DomainEvents.SampleConsoleApp.FakeDomainLayer.Entities;

namespace DomainEvents.SampleConsoleApp.FakeDomainLayer
{
    public interface IAccountFetcher
    {
        Account FetchById(long id);
    }
}