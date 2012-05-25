using System;

namespace DomainEvents.Testing
{
    public class EventNotDispatchedException<T> : Exception
    {
        public EventNotDispatchedException() : base("There were no events of type '" + typeof(T).Name + "' dispatched in the test dispatcher.")
        {            
        }
    }
}