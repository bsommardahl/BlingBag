using System;
using BlingBag.SampleConsoleApp.FakeDomainLayer;
using BlingBag.SampleConsoleApp.FakeDomainLayer.Entities;

namespace BlingBag.SampleConsoleApp.FakeDataLayer
{
    public class StubRepository : IRepository
    {
        #region IRepository Members

        public T Get<T>(long id) where T : Account
        {
            var account = new Account
                {
                    Id = id,
                    Name = "Bob",
                    EmailAddress = "bob@mycompany.com"
                };
            return (T) account;
        }

        public void Update<T>(T item) where T : Account
        {
            Console.WriteLine("## (StubRepository) -- The account was updated in the repository.");
        }

        #endregion
    }
}