using System;
using BlingBag.SampleConsoleApp.FakeDomainLayer.Events;

namespace BlingBag.SampleConsoleApp.FakeDomainLayer.Entities
{
    public class Account
    {
        public string Name { get; set; }

        public long Id { get; set; }

        public string EmailAddress { get; set; }

        public event Action<object> NotifyObservers;

        public void ChangeName(string newName)
        {
            string oldName = Name;
            Name = newName;

            NotifyObservers(new TheNameChanged(this, oldName, newName));
        }
    }
}