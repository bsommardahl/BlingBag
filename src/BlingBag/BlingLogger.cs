using System;

namespace BlingBag
{
    public class BlingLogger
    {
        static Action<Error> _logException = ex => Console.WriteLine(ex.Exception.Message);
        static Action<Info> _logInfo = info => Console.WriteLine(info.Message);

        public static Action<Error> LogException
        {
            get
            {
                return _logException;
            }
            set { _logException = value; }
        }

        public static Action<Info> LogInfo
        {
            get
            {
                _logInfo = info => Console.WriteLine(info.Message);
                return _logInfo;
            }
            set { _logInfo = value; }
        }
    }
}