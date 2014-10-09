using System;

namespace BlingBag
{
    public class Info
    {
        public string Message { get; private set; }
        public DateTime DateTime { get; private set; }

        public Info(string message, DateTime dateTime)
        {
            Message = message;
            DateTime = dateTime;
        }
    }
}