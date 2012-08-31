using System;
using BlingBag.SampleConsoleApp.FakeDomainLayer.Entities;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer
{
    public class InitializedAccountFetcher : IAccountFetcher
    {
        readonly IBlingInitializer<Action<object>> _initializer;
        readonly IRepository _repository;

        public InitializedAccountFetcher(IBlingInitializer<Action<object>> initializer, IRepository repository)
        {
            _initializer = initializer;
            _repository = repository;
        }

        #region IAccountFetcher Members

        public Account FetchById(long id)
        {
            var account = _repository.Get<Account>(id);
            return _initializer.Initialize(account);
        }

        #endregion
    }
}