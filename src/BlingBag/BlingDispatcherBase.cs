using System;
using System.Collections;

namespace BlingBag
{
    public abstract class BlingDispatcherBase : IBlingDispatcher
    {
        #region IBlingDispatcher Members

        public void Dispatch(object @event)
        {
            IEnumerable matchingBlingHandlers = GetMatchingBlingHandlers(@event);
            foreach (dynamic handler in matchingBlingHandlers)
            {
                BlingLogger.LogInfo(new Info("Starting dispatch.", DateTime.Now));
                try
                {
                    handler.Handle(@event);
                    BlingLogger.LogInfo(new Info("Finished dispatch.", DateTime.Now));
                }
                catch (Exception ex)
                {
                    BlingLogger.LogException(new Error(ex, DateTime.Now));
                    throw;
                }
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