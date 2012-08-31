using System;
using BlingBag.StructureMap;
using StructureMap.Configuration.DSL;

namespace BlingBag.SampleConsoleApp.Infrastructure
{
    public class StandardDomainEventsConfiguration : Registry
    {
        public StandardDomainEventsConfiguration()
        {
            For<IBlingInitializer<Action<object>>>().Use<BlingInitializer<Action<object>>>();
            For<IBlingConfigurator<Action<object>>>().Use<DefaultBlingConfigurator>();
            For<IBlingDispatcher>().Use<StructureMapBlingDispatcher>();
        }
    }
}