DomainEvents
============

### The Problem:
As an excellent software developer, you want to be sure that your code is maintainable, testable, expressive, and beautiful. But sometimes those desires seem to conflict with one another. For instance, an expressive domain should contain entities that, not only include well-named properties, but also the behavior of the business's domain. But, if you include behavior in your domain entities, you run the risk of harming the application's maintainability by having classes with more than one "reason to change" or "responsibility" (see Single Responsibility Principle). 

### The Solution:
Well, the solution really has two parts: 1) Double-Dispatch and 2) Domain Events. Since this wiki is all about Domain Events, I'll let you read about Double-Dispatch on your own and stick to the subject at hand. So, consider the following POCO class:

```csharp
public class Account
{
    public string Name { get; set; }
    public long Id { get; set; }
    public string EmailAddress { get; set; }
}
``` 

The above POCO is how all of my domain entities used to look. It's clean, but boring... this class does absolutely nothing. Before I met Domain Events, I would have created any number of domain "services" that performed actions on this Account class. The problem with this is that, in order to read the code and decypher what the Account does, you have to go service fishing and search for all the services that touch Account. It's a bit of a shame that we have such a clean code base, but one that expresses very little of the actual domain.

Now, let's add some behavior. Our imaginary business has passed down a new requirement from on high: 
"As an account, I can change my name."

So, consider the same POCO class with a bit of behavior:

```csharp
public class Account
{
    public string Name { get; set; }
    public long Id { get; set; }
    public string EmailAddress { get; set; }

    public void ChangeName(string newName)
    {
        Name = newName;
    }
}
``` 

This is great. We're starting to make our Account entity a little more interesting. But, we have done very little and are pretty restricted to what we can actually do. But we still have three more pieces of the requirement to implement (found in a pencil-written footnote):

When account name changes...
- account should be saved to the database.
- should email the account owner notifying of the change
- should log the change

How can we do all that from inside a POCO domain entity? Well, the answer is that you can't (or shouldn't). BUT, what you CAN do is send out a signal to the rest of the domain that the name has changed and allow the domain to respond to the signal.

So, consider the same POCO, now equipped with DomainEvents:

```csharp
public class Account
{
    public string Name { get; set; }
    public long Id { get; set; }
    public string EmailAddress { get; set; }

    public event DomainEvent NotifyObservers;

    public void ChangeName(string newName)
    {
        var oldName = Name;
        Name = newName;
        NotifyObservers(new TheNameChanged(this, oldName, newName));
    }
}
``` 

## Behavior-Rich Domain Model - BAM!
That's more like it! Now, our domain entity has behavior that expresses the requirements of the business. At this point, any number of event handlers (observers) can respond to the TheNameChanged "event". To fulfill our client's requirements, here are some possible event handlers:

* UpdateTheAccountInTheDatabaseAfterNameChange
* EmailAccountOwnerAfterNameChange
* LogThatAccountNameWasChanged

For more information on how to install, implement or use DomainEvents, [check out the wiki](wiki/home).

For sample code, check out the [sample app in the source code](https://github.com/bsommardahl/DomainEvents/tree/master/src/DomainEvents.SampleConsoleApp).