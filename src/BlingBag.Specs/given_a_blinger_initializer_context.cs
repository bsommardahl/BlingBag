using Machine.Specifications;

namespace BlingBag.Specs
{
    public abstract class given_a_blinger_initializer_context : given_an_initializer_context<Blinger>
    {
        Establish master_context = () =>
            {
                Blinger handleEvent = x => EventsHandled.Add(x);
                MockedBlingConfigurator.Setup(x => x.HandleEvent).Returns(handleEvent);
            };
    }
}