namespace BlingBag.Specs
{
    public class TestHandler : IBlingHandler<TestClass>
    {
        public object EventHandled;

        public void Handle(TestClass @event)
        {
            EventHandled = @event;
        }        
    }
}