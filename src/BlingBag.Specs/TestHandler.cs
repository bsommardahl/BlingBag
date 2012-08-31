namespace BlingBag.Specs
{
    public class TestHandler : IBlingHandler<TestClass>
    {
        public object EventHandled;

        #region IBlingHandler<TestClass> Members

        public void Handle(TestClass @event)
        {
            EventHandled = @event;
        }

        #endregion
    }
}