using System;
using DomainEvents.SampleConsoleApp.FakeDomainLayer.Events;

namespace DomainEvents.SampleConsoleApp.FakeDomainLayer.EventHandlers
{
    public class LogThatNameChanged : IDomainEventHandler<TheNameChanged>
    {
        public void Handle(TheNameChanged @event)
        {
            Console.WriteLine(string.Format("## (LogThatNameChanged) -- The name '{0}' changed to '{1}'.", @event.OldName, @event.NewName));
        }
    }
}