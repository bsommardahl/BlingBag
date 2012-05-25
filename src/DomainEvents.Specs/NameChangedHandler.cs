namespace DomainEvents.Specs
{
    public class NameChangedHandler : IDomainEventHandler<NameChanged>
    {
        #region IEventHandler<NameChanged> Members

        public void Handle(NameChanged @event)
        {
        }

        #endregion
    }
}