using BlingBag.SampleConsoleApp.FakeDomainLayer.Entities;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer
{
    public interface IRepository
    {
        T Get<T>(long id) where T : Account;
        void Update<T>(T item) where T : Account;
    }
}