using System;

namespace DomainEvents.SampleConsoleApp
{
    public class LogThatNameChanged : IDomainEventHandler
    {
        public Type Handles
        {
            get { return typeof (TheNameChanged); }
        }

        public void Handle(object @event)
        {
            var ev = (TheNameChanged) @event;
            Console.WriteLine(string.Format("## (LogThatNameChanged) -- The name '{0}' changed to '{1}'.", ev.OldName, ev.NewName));
        }
    }
}