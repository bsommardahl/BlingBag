DomainEvents
============

Inspired by:

Bootstrapping:

You have to initialize the domain events on your domain entities before they can be handled. To do that, you need to use the IDomainEventInitialize at some key point in your domain. It's possible to inject the initializer in your repository or a special domain service that's meant to fetch a domain entity.

Main Pieces:

DomainEvent - 

IDomainEventHandler<T> - 

IDomainEventDispatcher -

IDomainEventInitializer - 