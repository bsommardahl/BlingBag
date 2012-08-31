using System.Collections.Generic;
using System.Linq;
using BlingBag.Testing;
using Machine.Specifications;
using Moq;

namespace BlingBag.Specs
{
    public abstract class given_a_bling_initializer_context
    {
        protected static BlingInitializer Initializer;
        protected static Mock<IBlingConfigurator> MockedBlingConfigurator;
        protected static List<object> EventsHandled = new List<object>();

        Establish master_context = () =>
            {
                MockedBlingConfigurator = new Mock<IBlingConfigurator>();
                Initializer = new BlingInitializer(MockedBlingConfigurator.Object);

                MockedBlingConfigurator.Setup(x => x.EventSelector).Returns(x => x.EventHandlerType == typeof (Blinger));
                Blinger handleEvent = x => EventsHandled.Add(x);
                MockedBlingConfigurator.Setup(x => x.HandleEvent).Returns(handleEvent);
            };

        public static List<T> ShouldHaveHandled<T>()
        {
            return EventsHandled.Where(x => x.GetType() == typeof(T)).Cast<T>().ToList();
        }
        
    }
}