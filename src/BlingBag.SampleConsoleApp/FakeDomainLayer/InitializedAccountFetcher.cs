using BlingBag.SampleConsoleApp.FakeDomainLayer.Entities;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer
{
    public class InitializedAccountFetcher : IAccountFetcher
    {
        readonly IBlingInitializer _initializer;
        readonly IRepository _repository;

        public InitializedAccountFetcher(IBlingInitializer initializer, IRepository repository)
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