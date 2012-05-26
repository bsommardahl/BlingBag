namespace DomainEvents
{
    public class DefaultDomainEventDispatcher : IDomainEventDispatcher
    {
        #region IDomainEventDispatcher Members

        public void Dispatch<T>(T @event)
        {
            var handlers = DomainEventHandlers.GetFor(@event);
            foreach (IDomainEventHandler<T> handler in handlers)
            {
                handler.Handle(@event);
            }
        }

        #endregion
    }
}