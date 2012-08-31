using BlingBag.SampleConsoleApp.FakeDomainLayer.Events;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer.EventHandlers
{
    public class EmailTheAccountHolderAfterNameChanged : IBlingHandler<TheNameChanged>
    {
        readonly IEmailClient _emailClient;

        public EmailTheAccountHolderAfterNameChanged(IEmailClient emailClient)
        {
            _emailClient = emailClient;
        }

        #region IBlingHandler<TheNameChanged> Members

        public void Handle(TheNameChanged @event)
        {
            _emailClient.Send(@event.Account.EmailAddress, "Name Changed",
                              string.Format("Your name has been changed from {0} to {1}.", @event.OldName,
                                            @event.NewName));
        }

        #endregion
    }
}