using DomainEvents.SampleConsoleApp.FakeDataLayer;
using DomainEvents.SampleConsoleApp.FakeDomainLayer;
using DomainEvents.SampleConsoleApp.FakeDomainLayer.EventHandlers;
using DomainEvents.SampleConsoleApp.FakeDomainLayer.Events;
using StructureMap;

namespace DomainEvents.SampleConsoleApp.Infrastructure
{
    public class Bootstrapper
    {
        readonly IContainer _container;

        public Bootstrapper(IContainer container)
        {
            _container = container;                        
        }

        public void Run()
        {
            _container.Configure(x =>
                {
                    x.AddRegistry<StandardDomainEventsConfiguration>();
                    x.For<IDomainEventHandler<TheNameChanged>>().Use<LogThatNameChanged>();
                    x.For<IDomainEventHandler<TheNameChanged>>().Use<UpdateAccountAfterNameChange>();
                    x.For<IDomainEventHandler<TheNameChanged>>().Use<EmailTheAccountHolderAfterNameChanged>();

                    x.For<IRepository>().Use<StubRepository>();
                    x.For<IAccountFetcher>().Use<InitializedAccountFetcher>();
                    x.For<IEmailClient>().Use<FakeEmailClient>();
                });
        }
    }
}