using System.Collections;
using System.Reflection;

namespace BlingBag
{
    public class DefaultBlingDispatcher : IBlingDispatcher
    {
        #region IBlingDispatcher Members

        public void Dispatch(object @event)
        {
            MethodInfo method = typeof (BlingHandlers).GetMethod("GetFor");
            MethodInfo generic = method.MakeGenericMethod(@event.GetType());
            var handlers = (IList) generic.Invoke(null, new[] {@event});

            foreach (object handler in handlers)
            {
                MethodInfo handlerMethod = handler.GetType().GetMethod("Handle");
                handlerMethod.Invoke(handler, new[] {@event});
            }
        }

        #endregion
    }
}