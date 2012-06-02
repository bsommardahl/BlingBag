using System;

namespace DomainEvents.SampleConsoleApp
{
    public class LogThatNameChanged : IDomainEventHandler<TheNameChanged>
    {
        public void Handle(TheNameChanged @event)
        {
            Console.WriteLine(string.Format("## (LogThatNameChanged) -- The name '{0}' changed to '{1}'.", @event.OldName, @event.NewName));
        }
    }
}