using System;
using System.Collections;
using StructureMap;

namespace BlingBag.StructureMap
{
    public class StructureMapBlingDispatcher : BlingDispatcherBase
    {
        readonly IContainer _container;

        public StructureMapBlingDispatcher(IContainer container)
        {
            _container = container;
        }

        protected override IEnumerable ResolveAll(Type blingHandlerType)
        {
            return _container.GetAllInstances(blingHandlerType);
        }
    }
}