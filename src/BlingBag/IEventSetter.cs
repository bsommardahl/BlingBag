using System.Collections.Generic;

namespace BlingBag
{
    public interface IEventSetter
    {
        void Set<TClass>(TClass obj, object @delegate, HashSet<object> seen) where TClass : class;
    }
}