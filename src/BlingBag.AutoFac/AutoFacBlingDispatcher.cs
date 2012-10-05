using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;

namespace BlingBag.AutoFac
{
    public class AutoFacBlingDispatcher : BlingDispatcherBase
    {
        readonly ILifetimeScope _container;

        public AutoFacBlingDispatcher(ILifetimeScope container)
        {
            _container = container;
        }

        protected override IEnumerable ResolveAll(Type blingHandlerType)
        {
            return _container.Resolve(typeof (IEnumerable<>).MakeGenericType(blingHandlerType)) as IEnumerable;
        }
    }
}