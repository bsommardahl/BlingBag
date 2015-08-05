using System;

namespace BlingBag
{
    public interface IBlingLogger
    {
        void LogException(object handler, DateTime timeStamp, Exception exception);
        void LogInfo(object handler, DateTime timeStamp, string message);
    }
}