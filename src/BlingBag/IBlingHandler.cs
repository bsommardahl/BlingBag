using System.Threading.Tasks;

namespace BlingBag
{
    public interface IBlingHandler<in T>
    {
        Task Handle(T @event);
    }
}