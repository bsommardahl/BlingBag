using System;
using DomainEvents.SampleConsoleApp.FakeDomainLayer;
using DomainEvents.SampleConsoleApp.FakeDomainLayer.Entities;

namespace DomainEvents.SampleConsoleApp.FakeDataLayer
{
    public class StubRepository : IRepository
    {
        public T Get<T>(long id) where T:Account
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
    }
}