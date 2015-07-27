using System.Threading.Tasks;

namespace BlingBag.Specs
{
    public class NameChangedHandler : IBlingHandler<NameChanged>
    {
        #region IBlingHandler<NameChanged> Members

        public async Task Handle(NameChanged @event)
        {
        }

        #endregion
    }
}