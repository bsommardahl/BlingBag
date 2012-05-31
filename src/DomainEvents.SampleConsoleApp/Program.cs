using System;
using DomainEvents.StructureMap;
using StructureMap;

namespace DomainEvents.SampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //bootstrapper
            var container = new Container();
            container.Configure(x =>
                {                    
                    x.For<IDomainEventHandler>().Use<LogThatNameChanged>();
                });

            DomainEventHandlers.Resolve = x => container.GetInstance(x);            
            DomainEventHandlers.Register<TheNameChanged, LogThatNameChanged>();

            var dispatcher = new DefaultDomainEventDispatcher();
            var initializer = new DomainEventInitializer(dispatcher);

            var account = new Account("Bob");
            initializer.Initialize(account);

            Console.WriteLine("The account name is 'Bob'.");
            Console.WriteLine("");

            Console.ReadKey();

            Console.WriteLine("We're going to change the name to 'Robert'. Ready? Press a key.");
            Console.WriteLine("");

            Console.ReadKey();

            account.ChangeName("Robert");

            Console.WriteLine("");
            Console.WriteLine(
                "There. The name has been changed. There should have been an event thrown that logged the change to the screen.");

            Console.ReadKey();

        }
    }
}