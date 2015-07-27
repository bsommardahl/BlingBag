using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlingBag
{
    public abstract class BlingDispatcherBase : IBlingDispatcher
    {
        #region IBlingDispatcher Members

        public async Task Dispatch(object @event)
        {
            IEnumerable matchingBlingHandlers = GetMatchingBlingHandlers(@event);
            foreach (object handler in matchingBlingHandlers)
            {
                LogInfo(handler, DateTime.UtcNow, string.Format("Dispatching {0}...", handler.GetType().Name));
                try
                {
                    MethodInfo handlerMethod =
                        handler.GetType()
                            .GetMethods()
                            .First(
                                x => x.Name == "Handle" && x.GetParameters().First().ParameterType == @event.GetType());

                    await (Task) (handlerMethod.Invoke(handler, new[] {@event}));
                    LogInfo(handler, DateTime.UtcNow, string.Format("Finished {0}.", handler.GetType().Name));
                }
                catch (TargetInvocationException ex)
                {
                    LogException(handler, DateTime.UtcNow, ex.InnerException);
                    throw;
                }
                catch (AggregateException ex)
                {
                    LogException(handler, DateTime.UtcNow, ex.InnerException);
                    throw;
                }
                catch (Exception ex)
                {
                    LogException(handler, DateTime.UtcNow, ex);
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
        protected abstract void LogInfo(object handler, DateTime timeStamp, string message);
        protected abstract void LogException(object handler, DateTime timeStamp, Exception exception);
    }
}