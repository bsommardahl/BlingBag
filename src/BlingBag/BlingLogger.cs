using System;

namespace BlingBag
{
    public class BlingLogger
    {
        static Action<object, Error> _logException = (sender, ex) => Console.WriteLine(ex.Exception.Message);
        static Action<object, Info> _logInfo = (sender, info) => Console.WriteLine(info.Message);

        public static Action<object, Error> LogException
        {
            get { return _logException; }
            set { _logException = value; }
        }

        public static Action<object, Info> LogInfo
        {
            get { return _logInfo; }
            set { _logInfo = value; }
        }
    }
}