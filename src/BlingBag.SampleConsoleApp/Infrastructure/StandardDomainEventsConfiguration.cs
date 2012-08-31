using BlingBag.StructureMap;
using StructureMap.Configuration.DSL;

namespace BlingBag.SampleConsoleApp.Infrastructure
{
    public class StandardDomainEventsConfiguration : Registry
    {
        public StandardDomainEventsConfiguration()
        {
            For<IBlingInitializer>().Use<BlingInitializer>();
            For<IBlingConfigurator>().Use<DefaultBlingConfigurator>();
            For<IBlingDispatcher>().Use<StructureMapBlingDispatcher>();
        }
    }
}