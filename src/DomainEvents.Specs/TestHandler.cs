namespace DomainEvents.Specs
{
    public class TestHandler : IDomainEventHandler<object>
    {
        public object EventHandled;

        #region IDomainEventHandler<object> Members

        public void Handle(object @event)
        {
            EventHandled = @event;
        }

        #endregion
    }
}