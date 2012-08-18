using BlingBag.SampleConsoleApp.FakeDomainLayer.Entities;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer
{
    public interface IAccountFetcher
    {
        Account FetchById(long id);
    }
}