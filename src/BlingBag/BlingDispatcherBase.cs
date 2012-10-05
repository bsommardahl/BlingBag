using System;
using System.Collections;
using System.Reflection;

namespace BlingBag
{
    public abstract class BlingDispatcherBase : IBlingDispatcher
    {
        #region IBlingDispatcher Members

        public void Dispatch(object @event)
        {
            IEnumerable matchingBlingHandlers = GetMatchingBlingHandlers(@event);
            foreach (object handler in matchingBlingHandlers)
            {
                MethodInfo handlerMethod = handler.GetType().GetMethod("Handle");
                handlerMethod.Invoke(handler, new[] {@event});
            }
        }

        #endregion

        IEnumerable GetMatchingBlingHandlers(object @event)
        {
            Type handlerType = typeof (IBlingHandler<>);
            Type genericHandlerType = handlerType.MakeGenericType(@event.GetType());
            return ResolveAll(genericHandlerType);
        }

        protected abstract IEnumerable ResolveAll(Type blingHandlerType);
    }
}