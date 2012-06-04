using DomainEvents.SampleConsoleApp.FakeDomainLayer.Entities;

namespace DomainEvents.SampleConsoleApp.FakeDomainLayer
{
    public interface IRepository
    {
        T Get<T>(long id) where T : Account;
        void Update<T>(T item) where T : Account;
    }
}