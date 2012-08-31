using System;
using System.Collections;
using System.Reflection;
using StructureMap;

namespace BlingBag.StructureMap
{
    public class StructureMapBlingDispatcher : IBlingDispatcher
    {
        readonly IContainer _container;

        public StructureMapBlingDispatcher(IContainer container)
        {
            _container = container;
        }

        #region IBlingDispatcher Members

        public void Dispatch(object @event)
        {
            foreach (var handler in MatchingBlingHandlers(@event))
            {
                MethodInfo handlerMethod = handler.GetType().GetMethod("Handle");
                handlerMethod.Invoke(handler, new[] {@event});
            }
        }

        IList MatchingBlingHandlers(object @event)
        {
            Type handlerType = typeof (IBlingHandler<>);
            Type genericHandlerType = handlerType.MakeGenericType(@event.GetType());
            IList domainEventHandlers = _container.GetAllInstances(genericHandlerType);
            return domainEventHandlers;
        }

        #endregion
    }
}