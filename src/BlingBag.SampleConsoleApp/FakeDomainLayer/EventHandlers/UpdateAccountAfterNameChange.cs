using BlingBag.SampleConsoleApp.FakeDomainLayer.Events;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer.EventHandlers
{
    public class UpdateAccountAfterNameChange : IBlingHandler<TheNameChanged>
    {
        readonly IRepository _repository;

        public UpdateAccountAfterNameChange(IRepository repository)
        {
            _repository = repository;
        }

        #region IBlingHandler<TheNameChanged> Members

        public void Handle(TheNameChanged @event)
        {
            _repository.Update(@event.Account);
        }

        #endregion
    }
}