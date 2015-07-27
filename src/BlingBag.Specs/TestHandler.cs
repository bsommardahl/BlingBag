using System.Threading.Tasks;

namespace BlingBag.Specs
{
    public class TestHandler : IBlingHandler<TestClass>
    {
        public object EventHandled;

        #region IBlingHandler<TestClass> Members

        public async Task Handle(TestClass @event)
        {
            EventHandled = @event;
        }

        #endregion
    }
}