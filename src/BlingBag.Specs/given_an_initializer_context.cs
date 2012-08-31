using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Moq;

namespace BlingBag.Specs
{
    public abstract class given_an_initializer_context<TEventType>
    {
        protected static BlingInitializer<TEventType> Initializer;
        protected static Mock<IBlingConfigurator<TEventType>> MockedBlingConfigurator;
        protected static List<object> EventsHandled = new List<object>();

        Establish masterful_master_context = () =>
            {
                MockedBlingConfigurator = new Mock<IBlingConfigurator<TEventType>>();
                Initializer = new BlingInitializer<TEventType>(MockedBlingConfigurator.Object);

                MockedBlingConfigurator.Setup(x => x.EventSelector).Returns(
                    x => x.EventHandlerType == typeof (TEventType));
            };

        public static List<T> ShouldHaveHandled<T>()
        {
            return EventsHandled.Where(x => x.GetType() == typeof (T)).Cast<T>().ToList();
        }
    }
}