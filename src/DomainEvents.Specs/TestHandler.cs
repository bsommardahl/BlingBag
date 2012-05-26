namespace DomainEvents.Specs
{
    public class TestHandler : IDomainEventHandler<TestClass>
    {
        public object EventHandled;

        #region IDomainEventHandler<object> Members

        public void Handle(TestClass @event)
        {
            EventHandled = @event;
        }

        #endregion
    }
}