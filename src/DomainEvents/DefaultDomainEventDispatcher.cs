namespace DomainEvents
{
    public class DefaultDomainEventDispatcher : IDomainEventDispatcher
    {
        #region IDomainEventDispatcher Members

        public void Dispatch<T>(T @event)
        {
            foreach (var handler in DomainEventHandlers.GetFor(@event))
            {
                handler.Handle(@event);
            }
        }

        #endregion
    }
}