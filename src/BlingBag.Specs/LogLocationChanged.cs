using System.Threading.Tasks;

namespace BlingBag.Specs
{
    public class LogLocationChanged : IBlingHandler<LocationChanged>
    {
        #region IBlingHandler<LocationChanged> Members

        public async Task Handle(LocationChanged @event)
        {
        }

        #endregion
    }
}