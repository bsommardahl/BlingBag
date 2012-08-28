namespace BlingBag.Specs
{
    public class User
    {
        public virtual string Username { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string ConfirmationKey { get; set; }
        public virtual bool IsConfirmed { get; set; }
        public virtual Account Account { get; set; }
        public event Blinger Bling;

        public void DoSomething()
        {
            Bling("user did something");
        }
    }
}