using DomainEvents.SampleConsoleApp.FakeDomainLayer.Entities;

namespace DomainEvents.SampleConsoleApp.FakeDomainLayer
{
    public class InitializedAccountFetcher : IAccountFetcher
    {
        readonly IDomainEventInitializer _initializer;
        readonly IRepository _repository;

        public InitializedAccountFetcher(IDomainEventInitializer initializer, IRepository repository)
        {
            _initializer = initializer;
            _repository = repository;
        }

        public Account FetchById(long id)
        {
            var account = _repository.Get<Account>(id);
            return _initializer.Initialize(account);
        }
    }
}