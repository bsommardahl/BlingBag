using System;
using System.Collections;
using System.Linq;
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
                BlingLogger.LogInfo(new Info("Starting dispatch.", DateTime.Now));
                try
                {
                    MethodInfo handlerMethod =
                        handler.GetType()
                            .GetMethods()
                            .First(
                                x => x.Name == "Handle" && x.GetParameters().First().ParameterType == @event.GetType());

                    handlerMethod.Invoke(handler, new[] { @event });
                    BlingLogger.LogInfo(new Info("Finished dispatch.", DateTime.Now));
                }
                catch (TargetInvocationException ex)
                {
                    BlingLogger.LogException(new Error(ex.InnerException, DateTime.Now));
                    throw;
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