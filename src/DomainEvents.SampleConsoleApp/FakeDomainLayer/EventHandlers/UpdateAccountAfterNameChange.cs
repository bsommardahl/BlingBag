using DomainEvents.SampleConsoleApp.FakeDomainLayer.Events;

namespace DomainEvents.SampleConsoleApp.FakeDomainLayer.EventHandlers
{
    public class UpdateAccountAfterNameChange : IDomainEventHandler<TheNameChanged>
    {
        readonly IRepository _repository;

        public UpdateAccountAfterNameChange(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(TheNameChanged @event)
        {
            _repository.Update(@event.Account);
        }
    }
}