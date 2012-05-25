﻿using System;

namespace AcklenAvenue.DomainEvents.Specs
{
    public class Location
    {
        public string Address { get; private set; }

        public Account Account { get; set; }

        public event DomainEvent RaiseEvent;

        public void ChangeLocation(string address)
        {
            Address = address;
            RaiseEvent(new LocationChanged());
        }
    }

    public class LogLocationChanged : IDomainEventHandler<LocationChanged>
    {
        public void Handle(LocationChanged @event)
        {

        }
    }
}