DomainEvents
============

Installation:
-------------

http://nuget.org/packages/DomainEvents

```
install-package DomainEvents
```

License:
--------
Microsoft Public License (MS-PL) - http://www.opensource.org/licenses/MS-PL

Use:
----
To use DomainEvents in your domain entities, just add an event field to your domain entity class like this:

```csharp
public class Account {
    public event DomainEvent NotifyObservers;
}
````

We called the event “NotifyObservers” simply because that expresses what is happening. You can call the event field whatever you'd like. But, we recommend that you use a word or phrase that expresses a general notification of the subscribers or observers of the domain entity... not the specific behavior (i.e. “NotifyThatNameChanged” is not a good event field name since it's specific to a type of behavior. What happens if your domain entity has other types of behavior?)

Now, to use the new DomainEvent field on your entity, you could do something like this:

```csharp
public class Account
{
    public Account(string name) {
        Name = name;
    }

    public string Name { get; private set; }
        
    public event DomainEvent NotifyObservers;

    public void ChangeName(string newName) {
        var oldName = Name;
        Name = newName;

        //here, we're going to notify the users that the name changed... 
        NotifyObservers(new TheNameChanged {OldName = oldName, NewName = newName});
        //why even bother with a comment? The code is expressive enough, right?     
    }
}
```

On the other side of the equation, we have an event dispatcher that is responsible for finding any matching event handlers and “dispatching” them. Here's an example of an event handler that matches our “TheNameChanged” event:

```csharp
public class LogThatNameChanged : IDomainEventHandler<TheNameChanged>
{
    public void Handle(TheNameChanged @event) {
        Console.WriteLine(string.Format("## (LogThatNameChanged) -- The name '{0}' changed to '{1}'.", @event.OldName, @event.NewName));    
    }
}
```

This event handler accepts our TheNameChanged event and logs it to the console. 

There are two main pieces of infrastructure that glue everything together. They are 1) The Initializer, and 2) The Dispatcher.

The Dispatcher 
--------------
The Dispatcher is responsible for searching for possible event handler matches and executing them. 

The Initializer
---------------
The initializer is responsible for “wiring up” all the domain events in a given entity to the dispatcher. The current implementation searches a provided entity, drilling into child objects and collections, looking for domain event fields. When it finds a domain event field, it subscribes to it using the dispatcher as an “event handler” (of course, the dispatcher represents any number of actual event handlers). From there, the dispatcher decides how (and with which handler) to handle the event.

Implementation
--------------
We suggest that you ONLY use DomainEvents with actual domain entities. In our team's typical architecture, domain entities are always retrieved by some sort of domain-level “service”. Take an “IAccountFetcher” like this for example:

```csharp
public interface IAccountFetcher {
    Account FetchById(long id);
}
```

Assuming we always fetch accounts using this fetcher service, then implementing the DomainEvents infrastructure is fairly simple. You would inject the initializer in your service and initialize any entities that it returns. Like this:

```csharp
public class InitializedAccountFetcher : IAccountFetcher {

    IDomainEventInitializer _initializer;
    IRepository _repository;

    public InitializedAccountFetcher(IDomainEventInitializer initializer, IRepository repository) {
        _initializer =  initializer;
        _repository = repository;
    }	

    public Account FetchById(long id) {
        var account = _repository.Get(id);
        return _initializer.Initialize(account);
    }
}
```

You also need to be sure to register the initializer and dispatcher in your IOC container. If you're using structureMap, here's a suggested configuration (using a registry):

```csharp
public class StandardDomainEventsConfiguration : Registry
{
    public StandardDomainEventsConfiguration() {
        For<IDomainEventInitializer>().Use<DomainEventInitializer>();
        For<IDomainEventDispatcher>().Use<StructureMapDomainEventDispatcher>();
    }
}
```

All that's left is to register your handlers. If you're planning on using the “StructureMapDomainEventDispatcher”, you can do something like this in your app's bootstrapper (including adding the above registry):

```csharp
var container = new Container();
container.Configure(x => {
    x.AddRegistry<StandardDomainEventsConfiguration>();
    x.For<IDomainEventHandler<TheNameChanged>>().Use<LogThatNameChanged>();
});
```

Summary
-------
In summary, to implement DomainEvents, you must:

1. Register an implementation of IDomainEventInitializer in your IOC container.
2. Register an implementation of IDomainEventDispatcher in your IOC container.
3. Register all implementations of IDomainEventHandler<> in your IOC container.
4. Always “initialize” your domain entities using an injected IDomainEventInitializer.
5. Add just one DomainEvent event field to each domain entity with a name like “NotifyObservers”.
6. Start raising DomainEvents in your behavior-rich domain entities.

Sample Code
-----------
For some extra explanation, check out the DomainEvents.SampleConsoleApp in the source code.