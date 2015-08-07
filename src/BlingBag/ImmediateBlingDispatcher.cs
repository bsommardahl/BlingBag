using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BlingBag
{
    public class ImmediateBlingDispatcher : BlingDispatcherBase
    {
        public readonly IEnumerable<IBlingHandler> Handlers;
        readonly IBlingLogger _logger;

        public ImmediateBlingDispatcher(IEnumerable<IBlingHandler> handlers, IBlingLogger logger)
        {
            Handlers = handlers;
            _logger = logger;
        }

        protected override IEnumerable FindHandlers(object @event)
        {
            return
                Handlers.Where(
                    x =>
                        x.GetType()
                            .GetInterfaces()
                            .Any(i => typeof (IBlingHandler).IsAssignableFrom(i)
                                      && (i.GenericTypeArguments.Any()
                                          && i.GenericTypeArguments[0] == @event.GetType())));
        }

        protected override void LogInfo(object handler, DateTime timeStamp, string message)
        {
            _logger.LogInfo(handler, timeStamp, message);
        }

        protected override void LogException(object handler, DateTime timeStamp, Exception exception)
        {
            _logger.LogException(handler, timeStamp, exception);
        }
    }
}