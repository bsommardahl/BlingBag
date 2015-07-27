using System.Threading.Tasks;

namespace BlingBag
{
    public interface IBlingDispatcher
    {
        Task Dispatch(object @event);
    }
}