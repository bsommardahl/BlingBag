using System;
using BlingBag.SampleConsoleApp.FakeDomainLayer;
using BlingBag.SampleConsoleApp.FakeDomainLayer.Entities;
using BlingBag.SampleConsoleApp.Infrastructure;
using StructureMap;

namespace BlingBag.SampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //bootstrap the application
            var container = new Container();
            new Bootstrapper(container).Run();

            //fetch an initialized domain entity
            var accountFetcher = container.GetInstance<IAccountFetcher>();
            Account account = accountFetcher.FetchById(1);

            //show how a behavior-rich domain entity can do some cool stuff
            Console.WriteLine("The account name is 'Bob'.");
            Console.WriteLine("");

            Console.ReadKey();

            Console.WriteLine("We're going to change the name to 'Robert'. Ready? Press a key.");
            Console.WriteLine("");

            Console.ReadKey();

            account.ChangeName("Robert");

            Console.WriteLine("");
            Console.WriteLine(
                "There. The name has been changed. There should have been some domain events handled as a result.");

            Console.ReadKey();
        }
    }
}