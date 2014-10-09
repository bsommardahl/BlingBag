using System;

namespace BlingBag
{
    public class Error
    {
        public Exception Exception { get; private set; }
        public DateTime DateTime { get; private set; }

        public Error(Exception exception, DateTime dateTime)
        {
            Exception = exception;
            DateTime = dateTime;
        }
    }
}