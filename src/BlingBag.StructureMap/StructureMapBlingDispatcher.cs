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
            foreach (object handler in MatchingBlingHandlers(@event))
            {
                MethodInfo handlerMethod = handler.GetType().GetMethod("Handle");
                handlerMethod.Invoke(handler, new[] {@event});
            }
        }

        #endregion

        IList MatchingBlingHandlers(object @event)
        {
            Type handlerType = typeof (IBlingHandler<>);
            Type genericHandlerType = handlerType.MakeGenericType(@event.GetType());
            IList domainEventHandlers = _container.GetAllInstances(genericHandlerType);
            return domainEventHandlers;
        }
    }
}