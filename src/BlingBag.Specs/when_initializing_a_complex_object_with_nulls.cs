using Machine.Specifications;

namespace BlingBag.Specs
{
    public class when_initializing_a_complex_object_with_nulls : given_a_blinger_initializer_context
    {
        static User _user;
        static Account _account;

        Establish context = () =>
            {
                _account = new Account();

                _user = new User {Account = _account};
            };

        Because of = () => Initializer.Initialize(_user);

        It should_initialize_the_account = () =>
            {
                _account.DoSomething();
                ShouldHaveHandled<string>().ShouldContain("account did something");
            };

        It should_initialize_the_user = () =>
            {
                _user.DoSomething();
                ShouldHaveHandled<string>().ShouldContain("user did something");
            };
    }
}