using BlingBag.SampleConsoleApp.FakeDataLayer;
using BlingBag.SampleConsoleApp.FakeDomainLayer;
using BlingBag.SampleConsoleApp.FakeDomainLayer.EventHandlers;
using BlingBag.SampleConsoleApp.FakeDomainLayer.Events;
using StructureMap;

namespace BlingBag.SampleConsoleApp.Infrastructure
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
                    x.For<IBlingHandler<TheNameChanged>>().Use<LogThatNameChanged>();
                    x.For<IBlingHandler<TheNameChanged>>().Use<UpdateAccountAfterNameChange>();
                    x.For<IBlingHandler<TheNameChanged>>().Use<EmailTheAccountHolderAfterNameChanged>();

                    x.For<IRepository>().Use<StubRepository>();
                    x.For<IAccountFetcher>().Use<InitializedAccountFetcher>();
                    x.For<IEmailClient>().Use<FakeEmailClient>();
                });
        }
    }
}