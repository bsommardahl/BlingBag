using System.Threading.Tasks;

namespace BlingBag
{
    public interface IBlingHandler
    {
        
    }

    public interface IBlingHandler<in T> : IBlingHandler
    {
        Task Handle(T @event);
    }
}