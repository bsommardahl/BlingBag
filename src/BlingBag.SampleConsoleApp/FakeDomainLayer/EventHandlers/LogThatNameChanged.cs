using System;
using BlingBag.SampleConsoleApp.FakeDomainLayer.Events;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer.EventHandlers
{
    public class LogThatNameChanged : IBlingHandler<TheNameChanged>
    {
        #region IBlingHandler<TheNameChanged> Members

        public void Handle(TheNameChanged @event)
        {
            Console.WriteLine(string.Format("## (LogThatNameChanged) -- The name '{0}' changed to '{1}'.",
                                            @event.OldName, @event.NewName));
        }

        #endregion
    }
}