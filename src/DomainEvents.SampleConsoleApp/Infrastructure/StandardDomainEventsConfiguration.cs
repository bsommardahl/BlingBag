using DomainEvents.StructureMap;
using StructureMap.Configuration.DSL;

namespace DomainEvents.SampleConsoleApp.Infrastructure
{
    public class StandardDomainEventsConfiguration : Registry
    {
        public StandardDomainEventsConfiguration()
        {
            For<IDomainEventInitializer>().Use<DomainEventInitializer>();
            For<IDomainEventDispatcher>().Use<StructureMapDomainEventDispatcher>();
        }
    }
}